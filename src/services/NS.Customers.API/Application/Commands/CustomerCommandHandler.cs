﻿using FluentValidation.Results;
using MediatR;
using NS.Core.Messages;
using NS.Customers.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NS.Customers.API.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler, IRequestHandler<CreateCustomerCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid())
            {
                return command.ValidationResult;
            }

            var customer = new Customer(command.Id, command.Name, command.Email, command.Cpf);

            //var customerExists = await _customerRepository.GetByCpf(command.Cpf);

            //if (customerExists != null)
            //{
            //    AddError($"The CPF {customerExists.Cpf.Number} is already registered.");
            //    return ValidationResult;
            //}

            //_customerRepository.Create(customer);

            //customer.AddEvent(new CreatedCustomerEvent(command.Id, command.Name, command.Email, command.Cpf));

            //return await Save(_customerRepository.UnitOfWork);

            return command.ValidationResult;
        }
    }
}