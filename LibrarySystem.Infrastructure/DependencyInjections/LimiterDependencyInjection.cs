using System.Threading.RateLimiting;
using InventoryManagement.Domain.Entities;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace LibrarySystem.Infrastructure.DependencyInjections;
internal static class LimiterDependencyInjection
{
    internal static IServiceCollection AddLimiter(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        var loginLimiter = configuration.GetSection(LoginLimiter.SectionName).Get<LoginLimiter>();
        services.AddRateLimiter(opt =>
        {
            opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            opt.AddPolicy(loginLimiter!.PolicyName, httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Connection.RemoteIpAddress!.ToString(),
             factory: _ => new FixedWindowRateLimiterOptions
             {
                 PermitLimit = loginLimiter.PermitLimit,
                 Window = TimeSpan.FromSeconds(loginLimiter.Window), // Don't Queue Requests
             })).RejectionStatusCode = (int)loginLimiter.RejectionStatusCode;
        });

        var fixedLimiter = configuration.GetSection(FixedLimiter.SectionName).Get<FixedLimiter>();
        services.AddRateLimiter(opt =>
        {
            opt.AddPolicy(fixedLimiter!.PolicyName, httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Connection.RemoteIpAddress!.ToString(),
             factory: _ => new FixedWindowRateLimiterOptions
             {
                 PermitLimit = fixedLimiter.PermitLimit,
                 QueueLimit = fixedLimiter.PermitLimit,
                 QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                 Window = TimeSpan.FromSeconds(fixedLimiter.Window),
             })).RejectionStatusCode = (int)fixedLimiter.RejectionStatusCode;
        });

        var uTMLimiter = configuration.GetSection(UTMLimiter.SectionName).Get<UTMLimiter>();
        services.AddRateLimiter(opt =>
        {
            opt.AddPolicy(uTMLimiter!.PolicyName, httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Connection.RemoteIpAddress!.ToString(),
             factory: _ => new FixedWindowRateLimiterOptions
             {
                 PermitLimit = uTMLimiter.PermitLimit,
                 Window = TimeSpan.FromSeconds(uTMLimiter.Window),
             })).RejectionStatusCode = (int)uTMLimiter.RejectionStatusCode; // Don't Queue Requests
        });

        var loginCodeLimiter = configuration.GetSection(LoginCodeLimiter.SectionName).Get<LoginCodeLimiter>();
        services.AddRateLimiter(opt =>
        {
            opt.AddPolicy(loginCodeLimiter!.PolicyName, httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Connection.RemoteIpAddress!.ToString(),
             factory: _ => new FixedWindowRateLimiterOptions
             {
                 PermitLimit = loginCodeLimiter.PermitLimit,
                 Window = TimeSpan.FromSeconds(loginCodeLimiter.Window),
             })).RejectionStatusCode = (int)loginCodeLimiter.RejectionStatusCode; // Don't Queue Requests
        });
        return services;
    }
}
