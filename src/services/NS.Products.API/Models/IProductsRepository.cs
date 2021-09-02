using NS.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.Products.API.Models
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> GetById(Guid id);

        void Add(Product product);

        void Update(Product product);
    }
}