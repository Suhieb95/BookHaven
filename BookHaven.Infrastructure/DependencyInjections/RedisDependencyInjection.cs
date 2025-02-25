using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.Entities;
using BookHaven.Infrastructure.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BookHaven.Infrastructure.DependencyInjections;
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
            opt.InstanceName = "mydb";
        });
        return services;
    }
}
