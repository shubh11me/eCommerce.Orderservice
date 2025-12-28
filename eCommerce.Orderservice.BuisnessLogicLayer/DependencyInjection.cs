using eCommerce.Orderservice.BuisnessLogicLayer.HttpClients;
using eCommerce.Orderservice.BuisnessLogicLayer.Mappers;
using eCommerce.Orderservice.BuisnessLogicLayer.ServiceContracts;
using eCommerce.Orderservice.BuisnessLogicLayer.Services;
using eCommerce.Orderservice.BuisnessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Orderservice.BuisnessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBLLayer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHttpClient<UserMicroserviceClient>(options => options.BaseAddress = new Uri($"http://{configuration["userMicroserviceBaseUrl"]}:{configuration["userMicroservicePort"]}")); ;
            services.AddScoped<IOrdersService, OrdersService>();
            return services;
        }
    }
}
