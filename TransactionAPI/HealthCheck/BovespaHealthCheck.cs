using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TransactionAPI.HealthCheck
{
    public class BovespaHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = "https://www.b3.com.br/pt_br/para-voce";

                using HttpClient client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = true });
                using var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, "Sistema da Bovespa funcionando"));
                }
                else
                {
                    return Task.FromResult(new HealthCheckResult(HealthStatus.Degraded, "Sistema da Bovespa fora do ar"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new HealthCheckResult(HealthStatus.Unhealthy, "Sistema da Bovespa fora do ar", ex));
            }
        }
    }
}
