using Aurora.Api.Entities.Interfaces.Services;
using Aurora.Api.Entities.MethodEx;
using Aurora.Api.JsonConverters;
using Aurora.Api.MethodEx;
using Aurora.Api.Utils;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConfigurationSubstitution;
using DiceClub.Database.Context;
using DiceClub.Services.Modules;
using Microsoft.EntityFrameworkCore;
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
                logConfig = logConfig.MinimumLevel.Debug().WriteTo.Console();
            }
            else
            {
                logConfig = logConfig.MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Debug)
                    .WriteTo.Console(new JsonFormatter(renderMessage: true));
            }

            Log.Logger = logConfig.CreateLogger();

            builder.Configuration
                .AddEnvironmentVariables()
                .EnableSubstitutions();

            builder.Services.AddDbContextFactory<DiceClubDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DiceClubDb".ReplaceEnv()))
                    .UseCamelCaseNamingConvention();
            });

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

                containerBuilder.RegisterEasyNetQ(builder.Configuration.GetSection("MQ")["Host"]);

                if (builder.Environment.IsDevelopment())
                {
                    containerBuilder.ForceSetEnv("alpha");
                }
                containerBuilder.ForceSetEnv("production");
            });

            builder.Host.UseSerilog();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonConverterForType());
            });


            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var financeDb = scope.ServiceProvider.GetService<DiceClubDbContext>();

                await financeDb?.Database.MigrateAsync();

                var dbSeedService = scope.ServiceProvider.GetService<IDbSeedService>();

                await dbSeedService.ExecuteDbSeeds();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            await app.RunAsync();
        }
    }
}