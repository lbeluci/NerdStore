using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.Products.API.Models;
using NS.WebApi.Core.Identities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.Products.API.Controllers
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
            //Refit ApiException test
            //Polly retry test
            //Polly Circuit Breaker test
            //throw new Exception("TEST ERROR");

            return await _productsRepository.GetById(id);
        }
    }
}