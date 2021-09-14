using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace NS.Customers.API.Application.Events
{
    public class CustomerEventHandler : INotificationHandler<CreatedCustomerEvent>
    {
        public Task Handle(CreatedCustomerEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}