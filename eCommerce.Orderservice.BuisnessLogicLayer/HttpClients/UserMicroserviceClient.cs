using eCommerce.Orderservice.BuisnessLogicLayer.DTO;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System.Net.Http.Json;

namespace eCommerce.Orderservice.BuisnessLogicLayer.HttpClients
{
    public class UserMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserMicroserviceClient> _logger;
        public UserMicroserviceClient(HttpClient httpClient, ILogger<UserMicroserviceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<UserDTO> GetUserByUserId(Guid userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/Users/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    UserDTO user = await response.Content.ReadFromJsonAsync<UserDTO>();
                    if (user == null)
                    {
                        throw new Exception("User not found");
                    }
                    return user;
                }
            }
            catch(BrokenCircuitException ex)
            {
                _logger.LogInformation(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex,ex.Message);
            }

            return new UserDTO(Guid.Empty, "Temprory Unavailable", "Temprory Unavailable", "Temprory Unavailable");
        }
    }
}
