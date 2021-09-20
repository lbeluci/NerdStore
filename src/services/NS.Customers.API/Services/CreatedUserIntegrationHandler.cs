using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NS.Core.Mediator;
using NS.Core.Messages.Integration;
using NS.Customers.API.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NS.Customers.API.Services
{
    public class CreatedUserIntegrationHandler : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private IBus _bus;

        public CreatedUserIntegrationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            _bus.Rpc.RespondAsync<CreatedUserIntegrationEvent, ResponseMessage>(async request => new ResponseMessage(await CreateCustomer(request)));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> CreateCustomer(CreatedUserIntegrationEvent integrationEvent)
        {
            var createCustomerCommand = new CreateCustomerCommand(integrationEvent.Id, integrationEvent.Name, integrationEvent.Email, integrationEvent.Cpf);

            using var scope = _serviceScopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

            var success = await mediator.SendCommand(createCustomerCommand);

            return success;
        }
    }
}