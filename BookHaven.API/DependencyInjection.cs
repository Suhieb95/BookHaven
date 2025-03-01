using FluentValidation;
using FluentValidation.AspNetCore;
using BookHaven.API.Common;
using BookHaven.API.Common.Constants;
using BookHaven.API.Filters;
using BookHaven.API.Handlers;
using BookHaven.Application.DependencyInjection;
using BookHaven.Infrastructure.DependencyInjections;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using BookHaven.Application.Authentication.AuthRequirements;

namespace BookHaven.API;
internal static class DependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager configuration,
                                                             string environmentName, bool isDev)
    {
        ArgumentNullException.ThrowIfNull(services);

        ConfigurAppSettings(configuration, environmentName);

        services.AddInfrastructure(configuration, isDev)
                .AddApplication();
        services.AddSwaggerGen();
        services.AddSwaggerAuth();
        services.SetUploadSize();
        services.AddApiVerison();
        services.AddValidation();
        services.ConfigureIP();
        services.AddOpenApi();
        services.AddCorsPolicy(configuration);
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddControllers(options =>
        {
            options.Filters.Add<JwtValidationFilter>();
            options.Filters.Add<UserAgentFilter>();
        });

        services.AddScoped<LastLoginFilter>();
        services.AddRouting(opt =>
        {
            opt.LowercaseUrls = true; // lowercase routes 
            opt.LowercaseQueryStrings = true; // query string keys are automatically converted to lowercase
        });

        services.AddSingleton<IAuthorizationHandler, PermissionAuthoriztionHandler>();
        services.AddSingleton<IAuthorizationHandler, ExcludeNewUserAuthoriztionHandler>();

        services.AddAuthorizationBuilder()
            .AddPolicy(CustomPolicies.ExcludeNewUserPolicy, policy =>
                        policy.Requirements.Add(new ExcludeNewUserRequirement(CustomRoles.NewUser)));

        return services;
    }
    private static IServiceCollection ConfigureIP(this IServiceCollection services)
    {
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });
        return services;
    }
    private static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
        return services;
    }
    private static void ConfigurAppSettings(ConfigurationManager configuration, string environmentName)
    {
        configuration.SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables();
    }
    private static IServiceCollection SetUploadSize(this IServiceCollection services)
    {
        services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = 5242880);
        services.Configure<KestrelServerOptions>(options => options.Limits.MaxRequestBodySize = 5242880);
        return services;
    }
    private static IServiceCollection AddApiVerison(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = false;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        return services;
    }
    private static IServiceCollection AddSwaggerAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookHaven", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter the Bearer token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    []
                }
            });
        });

        return services;
    }
}