﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace NS.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services.AddSingleton<IMessageBus>(new MessageBus(connectionString));

            return services;
        }
    }
}