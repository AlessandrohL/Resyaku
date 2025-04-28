using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Features.Roles.Queries.GetAllRoles;
using Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetAllUsers;
using Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetUserById;
using Resyaku.Web.Extensions;
using Resyaku.Web.Mapper;
using Resyaku.Web.ViewModels.Users;

namespace Resyaku.Web.Controllers
{
    [Route("users")]
    public sealed class UserController(
        ISender sender,
        IValidator<CreateUserViewModel> createUserValidator,
        IValidator<UpdateUserViewModel> updateUserValidator)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery] GetAllUsersQueryParameters queryParameters,
            CancellationToken cancellationToken)
        {
            var pagedUsers = await sender.Send(new GetAllUsersQuery(queryParameters), cancellationToken);
            var usersViewModel = new GetAllUsersViewModel(queryParameters, pagedUsers);

            return View(usersViewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreateUser(CancellationToken cancellationToken)
        {
            var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
            var viewModel = new CreateUserViewModel(roles);

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(
            CreateUserViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var validationResult = await createUserValidator.ValidateAsync(viewModel, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
                viewModel.AvailableRoles = roles;

                return View(viewModel);
            }

            Result creationResult = await sender.Send(viewModel.ToCreateUserCommand(), cancellationToken);

            if (creationResult.IsFailure)
            {
                ViewBag.UserCreationError = creationResult.Error.Description;
                
                var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
                viewModel.AvailableRoles = roles;

                return View(viewModel);
            }

            TempData["User.Created"] = "Usuario registrado con éxito.";

            return RedirectToAction("Index");
        }

        [HttpGet("{userId}/update")]
        public async Task<IActionResult> UpdateUser(string userId, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out _)) return NotFound();

            var userInfoResult = await sender.Send(new GetUserByIdQuery(userId), cancellationToken);

            if (userInfoResult.IsFailure)
            {
                // TODO: Custom not found page.
                return NotFound();
            }

            var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
            var viewModel = new UpdateUserViewModel(userInfoResult.Value, roles);

            return View(viewModel);
        }

        [HttpPost("{userId}/update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(
            string userId,
            [FromForm] UpdateUserViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var validationResult = await updateUserValidator.ValidateAsync(viewModel, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
                viewModel.AvailableRoles = roles;

                return View(viewModel);
            }

            Result updateResult = await sender.Send(viewModel.ToUpdateUserCommand(userId), cancellationToken);

            if (updateResult.IsFailure)
            {
                ViewBag.UserUpdateError = updateResult.Error.Description;

                var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
                viewModel.AvailableRoles = roles;

                return View(viewModel);
            }

            TempData["User.Updated"] = "Usuario actualizado con éxito.";

            return RedirectToAction("Index");
        }
    }
}
