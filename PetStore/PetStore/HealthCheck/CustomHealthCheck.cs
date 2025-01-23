using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PetStore.HealthCheck
{
    public class CustomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            
            bool isHealthy = true;

            if (isHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("The service is healthy."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("The service is unhealthy."));
        }
    }
}
