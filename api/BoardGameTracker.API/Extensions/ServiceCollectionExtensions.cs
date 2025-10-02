using System.Text;
using BoardGameTracker.Common.Configuration;
using BoardGameTracker.Common.Contracts;
using BoardGameTracker.Data;
using BoardGameTracker.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace BoardGameTracker.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(s => s.GetService<ILogger<Program>>()!);

        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var hostSettings = configuration.GetSection(ConfigurationContract.HostSettings).Get<HostSettings>();
        services.AddCors(options =>
        {
            options.AddPolicy(ApiContract.CorsPolicy, policy =>
            {
                if (hostSettings is null || hostSettings.AllowedOrigins.Length == 0)
                {
                    policy.AllowAnyOrigin();
                }
                else
                {
                    policy.WithOrigins(hostSettings.AllowedOrigins);
                }

                policy.AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequireUppercase = true;
            o.Password.RequireNonAlphanumeric = true;
            o.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection(ConfigurationContract.JWTSettings);
        var secretKey = jwtConfig["Secret"];
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig["Issuer"],
                ValidAudience = jwtConfig["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? string.Empty))
            };
        });
    }

    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();
        builder.Host.UseSerilog();
    }

    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HostSettings>(configuration.GetSection(ConfigurationContract.HostSettings));
        services.Configure<JWTSettings>(configuration.GetSection(ConfigurationContract.JWTSettings));
    }
}