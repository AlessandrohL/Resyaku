using MediatR;
using Microsoft.AspNetCore.Identity;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.Errors;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Commands.LoginUser
{
    public sealed record LoginUserCommand(
        string Username,
        string Password) : IRequest<Result<ApplicationUser>>;

    public sealed class LoginUserCommandHandler(
        UserManager<ApplicationUser> userManager)
        : IRequestHandler<LoginUserCommand, Result<ApplicationUser>>
    {
        public async Task<Result<ApplicationUser>> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Username);

            if (user is null)
            {
                return Result.Failure<ApplicationUser>(UserErrors.NotFound);
            }

            if (!await userManager.CheckPasswordAsync(user, request.Password) || user.LockoutEnabled)
            {
                return Result.Failure<ApplicationUser>(UserErrors.InvalidCredentials);
            }

            return Result.Success(user);
        }
    }
}
