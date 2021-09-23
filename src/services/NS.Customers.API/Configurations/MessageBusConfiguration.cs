using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NS.Core.Utils;
using NS.Customers.API.Services;
using NS.MessageBus;

namespace NS.Customers.API.Configurations
{
    public static class MessageBusConfiguration
    {
        public static IServiceCollection AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));

            services.AddHostedService<CreatedUserIntegrationHandler>();

            return services;
        }
    }
}