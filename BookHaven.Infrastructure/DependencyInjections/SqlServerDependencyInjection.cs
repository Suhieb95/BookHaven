using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.Entities;
using BookHaven.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BookHaven.Infrastructure.DependencyInjections;
internal static class SqlServerDependencyInjection
{
    internal static IServiceCollection AddSqlServerDB(this IServiceCollection services, IConfiguration configuration, bool isDev)
    {
        ArgumentNullException.ThrowIfNull(services);

        string connectionString = configuration.GetSection(ConnectionString.SectionName).GetValue<string>(
                   isDev ? ConnectionString.LocalConnection : ConnectionString.ProductionConnection)!;

        services.AddScoped<IMSSQLConnectionFactory>(sp => new MSSQLConnectionFactory(connectionString!));
        services.AddScoped<ISqlDataAccess, SqlDataAccess>();
        services.AddScoped<IMssqlDbTransaction, SqlDataAccess>();
        return services;
    }
}