using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AiLab.Health;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly global::AiLab.Data.Context.AiLab _context;

    public DatabaseHealthCheck(global::AiLab.Data.Context.AiLab context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync(cancellationToken);
            return canConnect ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy("Cannot connect to database");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Exception while checking database connectivity", ex);
        }
    }
}
