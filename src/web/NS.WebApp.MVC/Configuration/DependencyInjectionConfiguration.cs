using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Services;
using NS.WebApp.MVC.Services.Handlers;

namespace NS.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IWebAppAuthenticationService, WebAppAuthenticationService>();

            services.AddHttpClient<IProductsService, ProductsService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}