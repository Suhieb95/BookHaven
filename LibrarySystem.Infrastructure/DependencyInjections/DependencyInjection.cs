using CloudinaryDotNet;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.Infrastructure.DependencyInjections;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isDev)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("Database");
        services.Configure<RefreshJwtSettings>(configuration.GetSection(RefreshJwtSettings.SectionName));
        services.AddLimiter(configuration);
        services.AddSqlServerDB(configuration, isDev);
        services.AddCloudinary(configuration);
        services.AddServices();
        services.AddAuthentication(configuration);
        services.AddRequestTimeouts(options =>
                                  {
                                      options.AddPolicy("default", new RequestTimeoutPolicy
                                      {
                                          Timeout = TimeSpan.FromSeconds(10),
                                          TimeoutStatusCode = StatusCodes.Status408RequestTimeout,
                                          WriteTimeoutResponse = async (context) =>
                                      {
                                          context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
                                          await context.Response.WriteAsync("Request Timed out.");
                                      }
                                      });
                                  });

        return services;
    }
    private static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        CloudinarySettings cloudinarySettings = configuration.GetSection(CloudinarySettings.SectionName).Get<CloudinarySettings>()!;
        Account acc = new(
                cloudinarySettings.CloudName,
                cloudinarySettings.ApiKey,
                cloudinarySettings.ApiSecret
            );

        Cloudinary cloudinary = new(acc);
        services.AddSingleton(cloudinary);

        return services;
    }
}
