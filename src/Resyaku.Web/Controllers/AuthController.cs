using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Features.Users.Commands.LoginUser;
using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Web.Extensions;
namespace Resyaku.Web.Controllers
{
    public sealed class AuthController(
        ISender mediator,
        IValidator<LoginUserCommand> loginValidator,
        SignInManager<ApplicationUser> signInManager,
        IHttpContextAccessor httpContextAccessor) : Controller
    {
        public IActionResult Index()
        {
            var claims = httpContextAccessor.HttpContext?.User;

            if (claims is not null && signInManager.IsSignedIn(claims))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await loginValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(command);
            }

            Result<ApplicationUser> result = await mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                ViewBag.AuthError = result.Error.Description;
                return View(command);
            }

            await signInManager.SignInAsync(user: result.Value, isPersistent: true);

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
