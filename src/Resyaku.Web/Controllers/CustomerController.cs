using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resyaku.Application.Features.Customers.Queries.GetCustomerByDni;

namespace Resyaku.Web.Controllers
{
    [Route("customers")]
    public sealed class CustomerController(ISender sender) : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet("by-dni/{dni}")]
        public async Task<IActionResult> GetCustomerByDni(string dni)
        {
            var result = await sender.Send(new GetCustomerByDniQuery(dni));

            if (result.IsFailure)
            {
                ModelState.AddModelError(result.Error.Code, result.Error.Description);
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
