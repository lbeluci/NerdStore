using FluentValidation.Results;
using NS.Core.Messages;
using System.Threading.Tasks;

namespace NS.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T eventToPublish) where T : Event;

        Task<ValidationResult> SendCommand<T>(T commandToSend) where T : Command;
    }
}