using NS.Core.DomainObjects;
using System;

namespace NS.Customer.API.Models
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Cpf { get; private set; }

        public bool Deleted { get; private set; }

        public Address Address { get; private set; }

        /*Remeber: This constructor is exclusive for EF*/
        protected Customer()
        {
        }

        public Customer(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
            Deleted = false;
        }

        public void ChangeEmail(string email)
        {
            Email = email;
        }

        public void SetAddress(Address address)
        {
            Address = address;
        }
    }
}
