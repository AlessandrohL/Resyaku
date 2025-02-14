using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Features.Roles.Queries.GetAllRoles;
using Resyaku.Infrastructure.Authentication.Features.Users.Commands.CreateUser;
using Resyaku.Infrastructure.Authentication.Features.Users.Commands.UpdateUser;
using Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetAllUsers;
using Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetUserById;
using Resyaku.Web.Extensions;
using Resyaku.Web.Mappers;
using Resyaku.Web.ViewModels.Users;

namespace Resyaku.Web.Controllers
{
    [Route("users")]
    public sealed class UserController(
        ISender sender,
        IValidator<CreateUserCommand> createUserValidator,
        IValidator<GetUserByIdQuery> getUserValidator,
        IValidator<UpdateUserViewModel> updateUserValidator)
        : Controller
    {
        public async Task<IActionResult> Index(
            [FromQuery] GetAllUsersQueryParameters queryParameters,
            CancellationToken cancellationToken)
        {
            var pagedUsers = await sender.Send(new GetAllUsersQuery(queryParameters), cancellationToken);
            var usersViewModel = new GetAllUsersViewModel(queryParameters, pagedUsers);
            return View(usersViewModel);
        }

        [Route("create")]
        public async Task<IActionResult> CreateUser(CancellationToken cancellationToken)
        {
            var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
            ViewBag.Roles = roles.Select(r => r.Name);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> CreateUser(
            CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await createUserValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
                ViewBag.Roles = roles.Select(r => r.Name);

                return View(command);
            }

            var creationResult = await sender.Send(command, cancellationToken);

            if (creationResult.IsFailure)
            {
                ViewBag.UserCreationError = creationResult.Error.Description;

                var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
                ViewBag.Roles = roles.Select(r => r.Name);

                return View(command);
            }

            TempData["User.Created"] = "Usuario registrado con éxito.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{userId}/update")]
        public async Task<IActionResult> UpdateUser(string userId, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(userId);
            var validationResult = await getUserValidator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
            {
                // TODO Custom NotFound page 
                return NotFound();
            }

            Result<GetUserByIdDto> queryResult = await sender.Send(query, cancellationToken);

            if (queryResult.IsFailure)
            {
                return NotFound();
            }

            var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
            ViewBag.Roles = roles.Select(r => r.Name);

            UpdateUserViewModel viewModel = queryResult.Value.ToUpdateUserViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{userId}/update")]
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
                ViewBag.Roles = roles.Select(r => r.Name);

                return View(viewModel);
            }

            UpdateUserCommand command = viewModel.ToUpdateUserCommand();
            var updateResult = await sender.Send(command, cancellationToken);

            if (updateResult.IsFailure)
            {
                ViewBag.UserUpdateError = updateResult.Error.Description;

                var roles = await sender.Send(new GetAllRolesQuery(), cancellationToken);
                ViewBag.Roles = roles.Select(r => r.Name);

                return View(viewModel);
            }

            TempData["User.Updated"] = "Usuario actualizado con éxito.";

            return RedirectToAction("Index");
        }
    }
}
