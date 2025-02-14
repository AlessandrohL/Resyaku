using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resyaku.Infrastructure.Authentication.Identity;

namespace Resyaku.Infrastructure.Authentication.Features.Roles.Queries.GetAllRoles
{
    public sealed class GetRolesQueryHandler(
        RoleManager<ApplicationRole> roleManager)
        : IRequestHandler<GetAllRolesQuery, IEnumerable<GetAllRolesDto>>
    {
        public async Task<IEnumerable<GetAllRolesDto>> Handle(
            GetAllRolesQuery request,
            CancellationToken cancellationToken)
        {
            return await roleManager
                .Roles
                .AsNoTracking()
                .Select(r => new GetAllRolesDto
                {
                    RoleId = r.Id.ToString(),
                    Name = r.Name!
                })
                .ToListAsync(cancellationToken);
        }
    }
}
