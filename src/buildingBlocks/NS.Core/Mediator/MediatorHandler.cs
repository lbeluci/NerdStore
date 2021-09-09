using FluentValidation.Results;
using MediatR;
using NS.Core.Messages;
using System.Threading.Tasks;

namespace NS.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T eventToPublish) where T : Event
        {
            await _mediator.Publish(eventToPublish);
        }

        public async Task<ValidationResult> SendCommand<T>(T commandToSend) where T : Command
        {
            return await _mediator.Send(commandToSend);
        }
    }
}