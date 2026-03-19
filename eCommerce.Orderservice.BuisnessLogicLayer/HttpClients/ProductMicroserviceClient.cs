using BuisnessLogicLayer.DTO;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Json;
using System.Text.Json;

namespace eCommerce.Orderservice.BuisnessLogicLayer.HttpClients
{
    public class ProductMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _distributedCache;
        public ProductMicroserviceClient(HttpClient httpClient,IDistributedCache distributedCache)
        {
            _httpClient = httpClient;
            _distributedCache = distributedCache;
        }
        public async Task<ProductResponse> GetProductByProductID(Guid? productID,CancellationToken token=default)
        {
            if(productID == null)
            {
                throw new ArgumentNullException(nameof(productID));
            }

            string productKey = $"product:{productID}";
            string cachedData = await _distributedCache.GetStringAsync(productKey, token);
            if (cachedData != null) {
               return JsonSerializer.Deserialize<ProductResponse>(cachedData);
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
                else
                {
                    cachedData= JsonSerializer.Serialize(product);
                    DistributedCacheEntryOptions distributedCacheEntry = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(300)).SetSlidingExpiration(TimeSpan.FromSeconds(100));
                   await _distributedCache.SetStringAsync(productKey, cachedData, distributedCacheEntry);
                }
                return product;
            }
        }
    }
}
