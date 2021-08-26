using Microsoft.Extensions.DependencyInjection;
using NS.ProductCatalog.API.Data;
using NS.ProductCatalog.API.Data.Repositories;
using NS.ProductCatalog.API.Models;

namespace NS.ProductCatalog.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ProductCatalogContext>();
        }
    }
}