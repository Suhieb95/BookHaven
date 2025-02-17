using LibrarySystem.Application.Interfaces.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LibrarySystem.Infrastructure.DataAccess;
public class DatabaseHealthCheck(ISqlDataAccess _ISqlDataAccess) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        int result = await _ISqlDataAccess.LoadSingle<int>("SELECT 1", cancellationToken: cancellationToken);

        if (result == 1)
            return HealthCheckResult.Healthy();
        else
            return HealthCheckResult.Unhealthy();
    }
}
