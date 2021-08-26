using Microsoft.EntityFrameworkCore;
using NS.Core.Data;
using NS.ProductCatalog.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.ProductCatalog.API.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductCatalogContext _context;

        public ProductsRepository(ProductCatalogContext context)
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