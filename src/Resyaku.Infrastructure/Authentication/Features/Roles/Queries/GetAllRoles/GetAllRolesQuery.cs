using MediatR;

namespace Resyaku.Infrastructure.Authentication.Features.Roles.Queries.GetAllRoles
{
    public record GetAllRolesQuery : IRequest<IEnumerable<GetAllRolesDto>>;
}
