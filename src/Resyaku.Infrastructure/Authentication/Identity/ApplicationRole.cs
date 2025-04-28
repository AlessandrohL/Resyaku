using Microsoft.AspNetCore.Identity;

namespace Resyaku.Infrastructure.Authentication.Identity
{
    public sealed class ApplicationRole : IdentityRole<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; } = [];
        public Guid RowGuid { get; init; } = Guid.NewGuid();

        public ApplicationRole() { }

        public ApplicationRole(string name)
        {
            Name = name;
        }
    }
}
