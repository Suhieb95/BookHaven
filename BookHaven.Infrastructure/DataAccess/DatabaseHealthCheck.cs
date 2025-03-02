using BookHaven.Application.Interfaces.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BookHaven.Infrastructure.DataAccess;
public class DatabaseHealthCheck(ISqlDataAccess sqlDataAccess) : IHealthCheck
{
    private readonly ISqlDataAccess _sqlDataAccess = sqlDataAccess;
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        int result = await _sqlDataAccess.LoadFirstOrDefault<int>("SELECT 1", cancellationToken: cancellationToken);

        if (result == 1)
            return HealthCheckResult.Healthy();
        else
            return HealthCheckResult.Unhealthy();
    }
}