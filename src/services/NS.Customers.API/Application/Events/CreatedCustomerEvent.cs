using NS.Core.Messages;
using System;

namespace NS.Customers.API.Application.Events
{
    public class CreatedCustomerEvent : Event
    {
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Cpf { get; private set; }

        public CreatedCustomerEvent(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }
    }
}