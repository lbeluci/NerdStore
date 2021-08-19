using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using System.Linq;

namespace NS.WebApp.MVC.Controllers
{
    public abstract class MainController : Controller
    {
        protected bool HasErrors(ResponseResult response)
        {
            if (response == null)
            {
                return false;
            }

            foreach (var message in response.Errors.Messages)
            {
                ModelState.AddModelError(string.Empty, message);
            }

            return response.Errors.Messages.Any();
        }
    }
}
