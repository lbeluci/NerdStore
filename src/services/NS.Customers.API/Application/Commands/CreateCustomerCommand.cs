using NS.Core.Messages;
using System;

namespace NS.Customers.API.Application.Commands
{
    public class CreateCustomerCommand : Command
    {
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Cpf { get; private set; }

        public CreateCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        //public override bool IsValid()
        //{
        //    ValidationResult = new CreateCustomerValidation().Validate(this);
        //    return ValidationResult.IsValid;
        //}
    }
}