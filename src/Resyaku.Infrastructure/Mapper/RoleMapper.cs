using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.DTOs.Roles;

namespace Resyaku.Infrastructure.Mapper
{
    public static class RoleMapper
    {
        public static RoleSummaryDto ToRoleSummary(this ApplicationRole role)
        {
            return new RoleSummaryDto(role.Id, role.Name!);
        }
    }
}
