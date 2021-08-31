using NS.WebApp.MVC.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public interface IProductsServiceRefit
    {
        [Get("/api/products/")]
        Task<IEnumerable<ProductViewModel>> GetAll();

        [Get("/api/products/{id}")]
        Task<ProductViewModel> GetById(Guid id);
    }
}