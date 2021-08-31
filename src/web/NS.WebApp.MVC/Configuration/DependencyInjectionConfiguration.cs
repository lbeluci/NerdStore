using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Services;
using NS.WebApp.MVC.Services.Handlers;
using Refit;
using System;

namespace NS.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IWebAppAuthenticationService, WebAppAuthenticationService>();

            //services.AddHttpClient<IProductsService, ProductsService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            /*REFIT CONFIGURATION TEST*/
            services.AddHttpClient("Refit", options => { options.BaseAddress = new Uri(configuration.GetSection("UrlProducts").Value); })
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddTypedClient(RestService.For<IProductsServiceRefit>);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}