using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.ProductCatalog.API.Models;
using NS.WebApi.Core.Identities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.ProductCatalog.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productRepository)
        {
            _productsRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet()]
        public async Task<IEnumerable<Product>> Index()
        {
            return await _productsRepository.GetAll();
        }

        [ClaimsAuthorize("ProductCatalog", "Read")]
        [HttpGet("{id:Guid}")]
        public async Task<Product> ProductDetails(Guid id)
        {
            return await _productsRepository.GetById(id);
        }
    }
}