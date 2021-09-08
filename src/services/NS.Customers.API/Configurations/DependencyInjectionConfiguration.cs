using Microsoft.Extensions.DependencyInjection;
using NS.Customers.API.Data;
//using NS.Customers.API.Data.Repositories;
//using NS.Customers.API.Models;

namespace NS.Customers.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<CustomersContext>();
        }
    }
}