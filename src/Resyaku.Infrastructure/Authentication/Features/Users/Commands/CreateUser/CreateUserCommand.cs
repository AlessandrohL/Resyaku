using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.Errors;
using Resyaku.Infrastructure.Extensions;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        string Firstname,
        string? Lastname,
        string Phone,
        string Email,
        string Username,
        string Password,
        IEnumerable<string> Roles) : IRequest<Result>;

    public sealed class CreateUserCommandHandler(UserManager<ApplicationUser> userManager)
        : IRequestHandler<CreateUserCommand, Result>
    {
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await userManager.IsUsernameAlreadyInUseAsync(request.Username))
            {
                return Result.Failure(UserErrors.UsernameAlreadyInUse);
            }

            if (await userManager.IsEmailAlreadyInUseAsync(request.Email))
            {
                return Result.Failure(UserErrors.EmailAlreadyInUse);
            }

            var newUser = new ApplicationUser(
                request.Firstname,
                request.Lastname,
                request.Username,
                request.Email,
                request.Phone);

            var creationResult = await userManager.CreateAsync(newUser, request.Password);

            if (!creationResult.Succeeded)
            {
                // TODO: Logging errors.
                var errors = creationResult.Errors.Select(e => e.Description);
                return Result.Failure(new Error(
                    "User.UnknownError",
                    ErrorType.Problem,
                    string.Join(',', errors)));
            }

            await userManager.AddClaimAsync(newUser, new Claim(
                nameof(newUser.Firstname),
                newUser.Firstname));

            if (!request.Roles.Any())
            {
                return Result.Success();
            }

            var roleAssignmentResult = await userManager.AddToRolesAsync(newUser, request.Roles);

            if (!roleAssignmentResult.Succeeded)
            {
                // TODO: Logging errors.
                var errors = creationResult.Errors.Select(e => e.Description);
                return Result.Failure(new Error(
                    "User.UnknownError",
                    ErrorType.Problem,
                    string.Join(',', errors)));
            }

            return Result.Success();
        }
    }
}
