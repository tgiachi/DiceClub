using System.Text;
using Aurora.Api.Entities.MethodEx;
using Aurora.Api.Utils;
using Aurora.Turbine.Api.Data;
using Aurora.Turbine.Api.MethodEx;
using Aurora.Turbine.Api.Services;
using DiceClub.Database.Context;
using DiceClub.Services.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace DiceClub.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            AssemblyUtils.AddAssembly(typeof(EventListenerModuleLoader).Assembly);

            var logConfig = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(new LoggingLevelSwitch(LogEventLevel.Information))
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                .Enrich.FromLogContext();

            var turbine = new TurbineWebEngine();

            var builder = await turbine.Build(new TurbineConfig()
            {

                IsMapControllers = true,
                UseSwagger = true
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            await turbine.InitLogger(logConfig);
            
            turbine.ConfigureServices((builder, config) =>
            {
                builder
                    .AddEntitiesDataAccess()
                    .AddDbSeedService()
                    .AddTurbineRestServices()
                    .AddDtoMappers();
                return builder;
            });
            
            turbine.OnTurbineAppBuilt += application => 
            {
                application.UseDefaultFiles();
                application.UseStaticFiles();
                
                application.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials()); // allow credentials

                
                application.UseAuthentication();
                application.UseAuthorization();
                

                return Task.CompletedTask;
            };

            turbine.AddContextFactory<DiceClubDbContext>(optionsBuilder =>
            {

                optionsBuilder
                    .UseNpgsql(
                        @"Server=127.0.0.1;Port=5432;Database=diceclub_db;User Id=postgres;Password=password;")
                    .UseSnakeCaseNamingConvention();
            }, true);



            builder.Services.AddApiVersioning(options => {
                options.AssumeDefaultVersionWhenUnspecified = true;

                options.DefaultApiVersion = new ApiVersion(1, 0);
                //    options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
            });


            await turbine.Run();
        }
    }
}