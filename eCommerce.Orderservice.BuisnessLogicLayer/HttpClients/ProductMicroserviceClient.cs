using BuisnessLogicLayer.DTO;
using System.Net.Http.Json;

namespace eCommerce.Orderservice.BuisnessLogicLayer.HttpClients
{
    public class ProductMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        public ProductMicroserviceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ProductResponse> GetProductByProductID(Guid? productID)
        {
            if(productID == null)
            {
                throw new ArgumentNullException(nameof(productID));
            }
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Product/{productID}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch product details");
            }
            else
            {
                ProductResponse product = await response.Content.ReadFromJsonAsync<ProductResponse>();
                if (product == null)
                {
                    throw new Exception("Product not found");
                }
                return product;
            }
        }
    }
}
