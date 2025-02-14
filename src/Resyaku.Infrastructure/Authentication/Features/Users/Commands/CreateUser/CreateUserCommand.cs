using MediatR;
using Resyaku.Domain.Primitives;

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
}
