using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.DTOs.Roles;

namespace Resyaku.Infrastructure.Authentication.Features.Roles.Queries.GetAllRoles
{
    public record GetAllRolesQuery : IRequest<List<RoleSummaryDto>>;

    public sealed class GetRolesQueryHandler(
        RoleManager<ApplicationRole> roleManager)
        : IRequestHandler<GetAllRolesQuery, List<RoleSummaryDto>>
    {
        public async Task<List<RoleSummaryDto>> Handle(
            GetAllRolesQuery request,
            CancellationToken cancellationToken)
        {
            return await roleManager
                .Roles
                .AsNoTracking()
                .Select(r => new RoleSummaryDto(r.Id, r.Name!))
                .ToListAsync(cancellationToken);
        }
    }
}
