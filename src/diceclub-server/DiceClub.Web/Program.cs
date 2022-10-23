using System.IO.Compression;
using System.Text;
using System.Text.Json.Serialization;
using Aurora.Api.Entities.Interfaces.Services;
using Aurora.Api.Entities.MethodEx;
using Aurora.Api.JsonConverters;
using Aurora.Api.MethodEx;
using Aurora.Api.Utils;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConfigurationSubstitution;
using DiceClub.Api.Data.Credentials;
using DiceClub.Database.Context;
using DiceClub.Services.Modules;
using DiceClub.Web.Controllers.WebSocket;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace DiceClub.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AssemblyUtils.AddAssembly(typeof(DefaultModuleLoader).Assembly);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var logConfig = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext();

            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsDevelopment())
            {
                logConfig = logConfig.MinimumLevel.Information().WriteTo.Console();
            }
            else
            {
                logConfig = logConfig.MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
                    .WriteTo.Console(new JsonFormatter(renderMessage: true));
            }

            Log.Logger = logConfig.CreateLogger();

            builder.Configuration
                .AddEnvironmentVariables()
                .EnableSubstitutions();


            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder
                    .ScanModules()
                    .AddEventBus()
                    .AddDtoMappers()
                    .AddEntitiesDataAccess()
                    // Clients
                    .AddDbSeedService()
                    .AddTaskQueueService();

               

                //  containerBuilder.RegisterEasyNetQ(builder.Configuration.GetSection("MQ")["Host"]);

                if (builder.Environment.IsDevelopment())
                {
                    containerBuilder.ForceSetEnv("alpha");
                }

                containerBuilder.ForceSetEnv("production");
            });

            builder.Host.UseSerilog();
            builder.Services.AddSingleton<IUserIdProvider, GuidUserIdProvider>();
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonConverterForType());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddDbContextFactory<DiceClubDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DiceClubDb".ReplaceEnv()))
                    .UseCamelCaseNamingConvention();
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/websocket"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }

                };
            });

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;

                options.DefaultApiVersion = new ApiVersion(1, 0);
                //    options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
            });

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddControllers();
            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            builder.Services.AddSignalR();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseResponseCompression();
            app.UseResponseCaching();

            app.MapHub<NotificationsHub>("/websocket/notifications");

            using (var scope = app.Services.CreateScope())
            {
                var financeDb = scope.ServiceProvider.GetService<DiceClubDbContext>();

                await financeDb!.Database.MigrateAsync();

                var dbSeedService = scope.ServiceProvider.GetService<IDbSeedService>();

                await dbSeedService!.ExecuteDbSeeds();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials


            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}