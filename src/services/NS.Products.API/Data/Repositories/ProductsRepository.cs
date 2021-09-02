using Microsoft.EntityFrameworkCore;
using NS.Core.Data;
using NS.Products.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.Products.API.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductsContext _context;

        public ProductsRepository(ProductsContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}