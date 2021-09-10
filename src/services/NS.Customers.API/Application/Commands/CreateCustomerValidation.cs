using FluentValidation;
using NS.Core.DomainObjects;
using System;

namespace NS.Customers.API.Application.Commands
{
    public class CreateCustomerValidation : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidation()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty).WithMessage("Invalid customer Id.");

            RuleFor(c => c.Name).NotEmpty().WithMessage("Customer name must be supplied.");

            RuleFor(c => c.Cpf).Must(IsCpfValid).WithMessage("Invalid customer CPF");

            RuleFor(c => c.Email).Must(IsEmailValid).WithMessage("Invalid customer E-Mail");
        }

        private static bool IsCpfValid(string cpf)
        {
            return Cpf.Validate(cpf);
        }

        private static bool IsEmailValid(string email)
        {
            return Email.Validate(email);
        }
    }
}