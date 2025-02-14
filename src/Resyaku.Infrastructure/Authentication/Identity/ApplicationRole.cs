using Microsoft.AspNetCore.Identity;

namespace Resyaku.Infrastructure.Authentication.Identity
{
    public sealed class ApplicationRole : IdentityRole<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; } = [];

        private ApplicationRole() { }

        public static ApplicationRole Create(string roleName)
        {
            return new ApplicationRole { Name = roleName };
        }
    }
}
