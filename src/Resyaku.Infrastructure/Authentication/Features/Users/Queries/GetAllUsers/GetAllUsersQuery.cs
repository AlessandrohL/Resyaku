using MediatR;
using Resyaku.Domain.Primitives;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetAllUsers
{
    public sealed record GetAllUsersQuery(GetAllUsersQueryParameters Parameters)
        : IRequest<PagedList<GetAllUsersDto>>;
}
