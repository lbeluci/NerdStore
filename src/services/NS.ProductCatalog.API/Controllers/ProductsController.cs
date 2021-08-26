using Microsoft.AspNetCore.Mvc;
using NS.ProductCatalog.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.ProductCatalog.API.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productRepository)
        {
            _productsRepository = productRepository;
        }

        [HttpGet("products")]
        public async Task<IEnumerable<Product>> Index()
        {
            return await _productsRepository.GetAll();
        }

        [HttpGet("products/{id:Guid}")]
        public async Task<Product> ProductDetails(Guid id)
        {
            return await _productsRepository.GetById(id);
        }
    }
}