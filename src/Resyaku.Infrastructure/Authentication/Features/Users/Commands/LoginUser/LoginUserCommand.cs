using MediatR;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Identity;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Commands.LoginUser
{
    public sealed record LoginUserCommand(
        string Username,
        string Password) : IRequest<Result<ApplicationUser>>;
}
