using NS.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.Customers.API.Models
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAll();

        Task<Customer> GetByCpf(string cpf);

        void Create(Customer customer);
    }
}