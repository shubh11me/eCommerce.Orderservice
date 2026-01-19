using eCommerce.Orderservice.BuisnessLogicLayer.HttpClients;
using eCommerce.Orderservice.BuisnessLogicLayer.Mappers;
using eCommerce.Orderservice.BuisnessLogicLayer.PolicyHandlers;
using eCommerce.Orderservice.BuisnessLogicLayer.ServiceContracts;
using eCommerce.Orderservice.BuisnessLogicLayer.Services;
using eCommerce.Orderservice.BuisnessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace eCommerce.Orderservice.BuisnessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBLLayer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddTransient<UserMicroservicePolicyHandler>();
            services.AddHttpClient<UserMicroserviceClient>(options => options.BaseAddress = new Uri($"http://{configuration["userMicroserviceBaseUrl"]}:{configuration["userMicroservicePort"]}")).AddPolicyHandler(
              services.BuildServiceProvider().GetRequiredService<UserMicroservicePolicyHandler>().GetRetryAsyncPolicy()
             ).AddPolicyHandler(services.BuildServiceProvider().GetRequiredService<UserMicroservicePolicyHandler>().GetCircuitBreakerPolicy());

            services.AddHttpClient<ProductMicroserviceClient>(options => options.BaseAddress = new Uri($"http://{configuration["productMicroserviceBaseUrl"]}:{configuration["productMicroservicePort"]}")); ;

            services.AddScoped<IOrdersService, OrdersService>();
            return services;
        }
    }
}
