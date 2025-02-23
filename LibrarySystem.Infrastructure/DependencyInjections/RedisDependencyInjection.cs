using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Infrastructure.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LibrarySystem.Infrastructure.DependencyInjections;
internal static class RedisDependencyInjection
{
    internal static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        string connectionString = configuration.GetSection(ConnectionString.SectionName).GetValue<string>(ConnectionString.RedisConnection)!;
        services.AddScoped<IRedisCacheService, RedisCacheService>();
        services.AddSingleton(opt => ConnectionMultiplexer.Connect(connectionString).GetDatabase()); // IDatabase
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = connectionString;
            opt.InstanceName = "Inventory-Management-";
        });
        return services;
    }
}
