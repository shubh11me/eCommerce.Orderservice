using eCommerce.Orderservice.BuisnessLogicLayer.DTO;
using System.Net.Http.Json;

namespace eCommerce.Orderservice.BuisnessLogicLayer.HttpClients
{
    public class UserMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        public UserMicroserviceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDTO> GetUserByUserId(Guid userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Users/{userId}");
            if (!response.IsSuccessStatusCode)
            {
              throw new Exception("Failed to fetch user details");
            }
            else
            {
                UserDTO user = await response.Content.ReadFromJsonAsync<UserDTO>();
                if(user == null)
                {
                    throw new Exception("User not found");
                }
                return user;
            }
        }
    }
}
