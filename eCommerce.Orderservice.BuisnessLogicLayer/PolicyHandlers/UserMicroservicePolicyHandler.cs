using Microsoft.Extensions.Logging;
using Polly;
namespace eCommerce.Orderservice.BuisnessLogicLayer.PolicyHandlers
{
    public class UserMicroservicePolicyHandler
    {
        ILogger<UserMicroservicePolicyHandler> _logger;
        public UserMicroservicePolicyHandler(ILogger<UserMicroservicePolicyHandler> logger)
        {
            _logger= logger;
        }
        public IAsyncPolicy<HttpResponseMessage> GetRetryAsyncPolicy() {
            return Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode).WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(2 + retryAttempt), (outcome, timespan, i, ctx) => { _logger.LogInformation($"Retring for the {i}th time"); });
        }
        public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode).CircuitBreakerAsync(3, TimeSpan.FromMinutes(2), (outcome, timespan) => { _logger.LogWarning("Breaking circuit"); }, () => { _logger.LogWarning("Restarting circuit"); });
        }
    }
}
