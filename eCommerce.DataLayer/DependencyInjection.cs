using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using eCommerce.Orderservice.dataLayer.RepositoryContracts;
using eCommerce.Orderservice.dataLayer.Repositories;
namespace eCommerce.Orderservice.dataLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var conf = configuration.GetConnectionString("MongoDb");
            conf = conf.Replace("$MONGO_HOST", Environment.GetEnvironmentVariable("MONGO_HOST")).Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGO_PORT"));
            services.AddSingleton<IMongoClient>(new MongoClient(conf));
            services.AddScoped<IMongoDatabase>(provider =>
            {
                IMongoClient client = provider.GetRequiredService<IMongoClient>();
                return client.GetDatabase("OrdersDatabase");
            });

            services.AddScoped<IOrdersRepository,OrdersRepository>();
            return services;
        }
    }
}
