using MediatR;
using Microsoft.AspNetCore.Identity;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.Errors;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Commands.UpdateUser
{
    public sealed class UpdateUserCommandHandler(UserManager<ApplicationUser> userManager)
        : IRequestHandler<UpdateUserCommand, Result>
    {
        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user is null)
            {
                return Result.Failure(UserErrors.NotFound);
            }

            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            user.UserName = request.Username;
            user.NormalizedUserName = request.Username.ToUpper();
            user.Email = request.Email;
            user.NormalizedEmail = request.Email.ToUpper();
            user.PhoneNumber = request.Phone;
            user.LockoutEnabled = request.LockoutEnabled;

            await userManager.UpdateAsync(user);

            if (request.ChangePassword)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, request.NewPassword);

                if (!result.Succeeded)
                {
                    return Result.Failure(UserErrors.PasswordNotUpdated);
                }
            }

            var currentRoles = await userManager.GetRolesAsync(user);
            var rolesToAdd = request.Roles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(request.Roles).ToList();

            if (rolesToAdd.Count != 0)
            {
                await userManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToRemove.Count != 0)
            {
                await userManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            return Result.Success();
        }
    }
}
