using Microsoft.AspNetCore.Mvc;
using NS.Core.Mediator;
using NS.Customers.API.Application.Commands;
using NS.WebApi.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace NS.Customers.API.Controllers
{
    [Route("api/customers")]
    public class CustomerController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomerController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.SendCommand(new CreateCustomerCommand(Guid.NewGuid(), "Luiz Beluci", "lbeluci@gmail.com", "32926106807"));

            return CustomResponse(result);
        }
    }
}