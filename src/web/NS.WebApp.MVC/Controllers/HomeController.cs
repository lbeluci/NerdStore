using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NS.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var error = new ErrorViewModel
            {
                ErrorCode = id
            };

            switch (id)
            {
                case 403:
                    error.Message = "Access denied! Do you have permission to do this?";
                    error.Title = "Access denied!";
                    break;
                case 404:
                    error.Message = "Page not found!";
                    error.Title = "Page not found!";
                    break;
                default:
                    error.Message = "Something went wrong! Try again later or tell us about the problem.";
                    error.Title = "Something went wrong!";
                    break;
            }

            return View(error);
        }
    }
}
