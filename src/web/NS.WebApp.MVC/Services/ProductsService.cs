using Microsoft.Extensions.Options;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public class ProductsService : Service, IProductsService
    {
        private readonly HttpClient _httpClient;

        public ProductsService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(appSettings.Value.UrlProducts);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/products");

            HandleErrorsResponse(response);

            return await DeserializeResponseMessageAsync<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/products/{id}");

            HandleErrorsResponse(response);

            return await DeserializeResponseMessageAsync<ProductViewModel>(response);
        }
    }
}