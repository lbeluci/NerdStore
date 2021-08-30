using NS.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();

        Task<ProductViewModel> GetById(Guid id);
    }
}