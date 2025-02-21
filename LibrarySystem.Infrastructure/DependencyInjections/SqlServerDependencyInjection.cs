using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace LibrarySystem.Infrastructure.DependencyInjections;
internal static class SqlServerDependencyInjection
{
    internal static IServiceCollection AddSqlServerDB(this IServiceCollection services, IConfiguration configuration, bool isDev)
    {
        ArgumentNullException.ThrowIfNull(services);

        string connectionString = configuration.GetSection(ConnectionString.SectionName).GetValue<string>(
                   isDev ? ConnectionString.LocalConnection : ConnectionString.ProductionConnection)!;

        services.AddScoped<IMssqlConnectionFactory>(sp => new MssqlConnectionFactory(connectionString!));
        services.AddScoped<ISqlDataAccess, SqlDataAccess>();
        services.AddScoped<IMssqlDbTransaction, SqlDataAccess>();
        return services;
    }
}
