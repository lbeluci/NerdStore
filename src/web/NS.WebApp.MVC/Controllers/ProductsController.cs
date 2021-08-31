using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Services;
using System;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Controllers
{
    public class ProductsController : MainController
    {
        //private readonly IProductsServiceRefit _productsService;

        //public ProductsController(IProductsServiceRefit productsService)
        //{
        //    _productsService = productsService;
        //}

        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        [Route("")]
        [Route("products")]
        public async Task<IActionResult> Index()
        {
            return View(await _productsService.GetAll());
        }

        [HttpGet]
        [Route("products/{id:Guid}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            return View(await _productsService.GetById(id));
        }
    }
}