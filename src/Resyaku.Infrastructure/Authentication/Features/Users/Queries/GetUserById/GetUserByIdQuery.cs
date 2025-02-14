using MediatR;
using Resyaku.Domain.Primitives;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(string UserId) : IRequest<Result<GetUserByIdDto>>;
}
