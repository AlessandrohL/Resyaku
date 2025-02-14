using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.Errors;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler(
        UserManager<ApplicationUser> userManager)
        : IRequestHandler<CreateUserCommand, Result>
    {
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var emailInUse = await IsEmailAlreadyInUseAsync(request.Email, cancellationToken);

            if (emailInUse)
            {
                return Result.Failure(UserErrors.UserAlreadyExists);
            }

            var newUser = ApplicationUser.Create(
                request.Firstname,
                request.Lastname,
                request.Phone,
                request.Email,
                request.Username);

            IdentityResult creationResult = await userManager.CreateAsync(newUser, request.Password);
            await userManager.AddClaimAsync(newUser, new Claim(
                nameof(newUser.Firstname),
                newUser.Firstname));

            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors.Select(e => e.Description);
                return Result.Failure(new Error(
                    "User.UnknownError",
                    ErrorType.Problem,
                    string.Join(',', errors)));
            }

            if (!request.Roles.Any())
            {
                return Result.Success();
            }

            IdentityResult roleAssignmentResult = await userManager.AddToRolesAsync(newUser, request.Roles);

            if (!roleAssignmentResult.Succeeded)
            {
                return roleAssignmentResult.Errors.FirstOrDefault() switch
                {
                    { Code: "UserAlreadyInRole" } => Result.Failure(UserErrors.AlreadyInRole),
                    _ => Result.Failure(RoleErrors.NotFound)
                };
            }

            return Result.Success();
        }

        private Task<bool> IsEmailAlreadyInUseAsync(
            string email,
            CancellationToken cancellationToken)
        {
            return userManager.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }
    }
}
