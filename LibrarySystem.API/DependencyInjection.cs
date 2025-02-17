using FluentValidation;
using FluentValidation.AspNetCore;
using LibrarySystem.API.Common;
using LibrarySystem.API.Filters;
using LibrarySystem.API.Handlers;
using LibrarySystem.Application;
using LibrarySystem.Infrastructure.DependencyInjections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace LibrarySystem.API;
internal static class DependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager configuration,
                                                             string environmentName, bool isDev)
    {
        ArgumentNullException.ThrowIfNull(services);

        configuration.SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables();

        services.AddInfrastructure(configuration, isDev)
                .AddApplication(configuration);

        services.AddControllers();
        services.AddOpenApi();
        services.Configure<ForwardedHeadersOptions>(options =>
          {
              options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
              options.KnownNetworks.Clear();
              options.KnownProxies.Clear();
          });
        services.AddEndpointsApiExplorer();
        services.AddApiVersioning(options =>
             {
                 options.AssumeDefaultVersionWhenUnspecified = true;
                 options.DefaultApiVersion = new ApiVersion(1, 0);
                 options.ReportApiVersions = false;
                 options.ApiVersionReader = new UrlSegmentApiVersionReader();
             });
        services.AddSwaggerGen();
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        services.AddHttpContextAccessor();
        services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
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
}
