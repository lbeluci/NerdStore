using Microsoft.EntityFrameworkCore;
using NS.Core.Data;
using NS.Customers.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.Customers.API.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersContext _context;

        public CustomerRepository(CustomersContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork { get; set; }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetByCpf(string cpf)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}