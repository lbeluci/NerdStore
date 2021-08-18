using Microsoft.Extensions.DependencyInjection;
using NS.WebApp.MVC.Services;

namespace NS.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddHttpClient<IWebAppAuthenticationService, WebAppAuthenticationService>();

            return services;
        }
    }
}