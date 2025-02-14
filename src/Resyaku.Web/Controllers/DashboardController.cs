using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Resyaku.Web.Controllers
{
    [Route("dashboard")]
    public sealed class DashboardController(
        IHttpContextAccessor httpContextAccessor) : Controller
    {
        public IActionResult Index()
        {
            var usernameClaim = httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name);

            ViewBag.Username = usernameClaim?.Value;
            return View();
        }
    }
}
