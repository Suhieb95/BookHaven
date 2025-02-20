using FluentValidation;
using FluentValidation.AspNetCore;
using LibrarySystem.API.Common;
using LibrarySystem.API.Filters;
using LibrarySystem.API.Handlers;
using LibrarySystem.Application.DependencyInjection;
using LibrarySystem.Infrastructure.DependencyInjections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace LibrarySystem.API;
internal static class DependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager configuration,
                                                             string environmentName, bool isDev)
    {
        ArgumentNullException.ThrowIfNull(services);

        ConfigurAppSettings(configuration, environmentName);

        services.AddInfrastructure(configuration, isDev)
                .AddApplication(configuration);

        services.SetUploadSize();
        services.AddApiVerison();
        services.AddValidation();
        services.ConfigureIP();
        services.AddOpenApi();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddControllers(options => options.Filters.Add<JwtValidationFilter>());

        // builder.Services.AddScoped<LastLoginFilter>();
        services.AddRouting(opt =>
        {
            opt.LowercaseUrls = true; // lowercase routes 
            opt.LowercaseQueryStrings = true; // query string keys are automatically converted to lowercase
        });
        services.AddSingleton<IAuthorizationHandler, PermissionAuthoriztionHandler>();

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
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 5242880; // Limit to 5 MB.
        });
        services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = 5242880; // Limit to 5 MB.
        });
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
}