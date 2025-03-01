using Account = CloudinaryDotNet.Account;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.Entities;
using BookHaven.Infrastructure.DataAccess;
using BookHaven.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Stripe;

namespace BookHaven.Infrastructure.DependencyInjections;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isDev)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddHealthChecks()
                .AddCheck<DatabaseHealthCheck>("Database");
        services.ConfigureOptions(configuration);
        services.AddLimiter(configuration);
        services.AddSqlServerDB(configuration, isDev);
        services.AddCloudinary(configuration);
        services.AddServices();
        services.AddRedis(configuration);
        services.AddAuthentication(configuration);
        services.AddHttpClient();
        services.AddScoped<IIPApiClient, IPApiClient>();
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
    private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

            services.Configure<AllowedAgent>(configuration.GetSection(AllowedAgent.SectionName));
        services.Configure<RefreshJwtSettings>(configuration.GetSection(RefreshJwtSettings.SectionName));
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
        services.Configure<IpProvider>(configuration.GetSection(IpProvider.SectionName));

        StripeSettings stripeSettings = new();
        configuration.Bind(StripeSettings.SectionName, stripeSettings);
        services.AddSingleton(Options.Create(stripeSettings));

        StripeConfiguration.ApiKey = stripeSettings.Secretkey;
    }
    private static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        CloudinarySettings cloudinarySettings = configuration.GetSection(CloudinarySettings.SectionName).Get<CloudinarySettings>()!;
        Account cloudinaryAccount = new(
             cloudinarySettings.CloudName,
             cloudinarySettings.ApiKey,
             cloudinarySettings.ApiSecret);

        services.AddSingleton(Options.Create(cloudinaryAccount));
        return services;
    }
}