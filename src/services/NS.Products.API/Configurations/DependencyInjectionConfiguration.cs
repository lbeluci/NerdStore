using Microsoft.Extensions.DependencyInjection;
using NS.Products.API.Data;
using NS.Products.API.Data.Repositories;
using NS.Products.API.Models;

namespace NS.Products.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ProductsContext>();
        }
    }
}