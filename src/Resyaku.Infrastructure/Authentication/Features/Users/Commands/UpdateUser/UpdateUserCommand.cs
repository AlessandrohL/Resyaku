using MediatR;
using Resyaku.Domain.Primitives;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(
        string UserId,
        string Firstname,
        string? Lastname,
        string Phone,
        string Email,
        string Username,
        bool ChangePassword,
        string NewPassword,
        bool LockoutEnabled,
        IEnumerable<string> Roles) : IRequest<Result>;
}
