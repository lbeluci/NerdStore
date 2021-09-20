using System;

namespace NS.Core.Messages.Integration
{
    public class CreatedUserIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Cpf { get; private set; }

        public CreatedUserIntegrationEvent(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }
    }
}