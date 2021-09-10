using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NS.Core.Mediator;
using NS.Customers.API.Application.Commands;
using NS.Customers.API.Data;
using NS.Customers.API.Data.Repositories;
using NS.Customers.API.Models;

namespace NS.Customers.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomersContext>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CustomerCommandHandler>();
        }
    }
}