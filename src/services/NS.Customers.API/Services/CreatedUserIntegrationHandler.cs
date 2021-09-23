using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NS.Core.Mediator;
using NS.Core.Messages.Integration;
using NS.Customers.API.Application.Commands;
using NS.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NS.Customers.API.Services
{
    public class CreatedUserIntegrationHandler : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMessageBus _bus;

        public CreatedUserIntegrationHandler(IServiceScopeFactory serviceScopeFactory, IMessageBus bus)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();

            return Task.CompletedTask;
        }

        private void OnConnected(object o, EventArgs e)
        {
            SetResponder();
        }

        private void SetResponder()
        {
            _bus.RespondAsync<CreatedUserIntegrationEvent, ResponseMessage>(async request => await CreateCustomer(request));

            _bus.AdvancedBus.Connected += OnConnected;
        }

        private async Task<ResponseMessage> CreateCustomer(CreatedUserIntegrationEvent integrationEvent)
        {
            var createCustomerCommand = new CreateCustomerCommand(integrationEvent.Id, integrationEvent.Name, integrationEvent.Email, integrationEvent.Cpf);

            using var scope = _serviceScopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

            var success = await mediator.SendCommand(createCustomerCommand);

            return new ResponseMessage(success);
        }
    }
}