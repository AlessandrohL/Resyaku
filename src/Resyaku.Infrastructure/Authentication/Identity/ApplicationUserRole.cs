using Microsoft.AspNetCore.Identity;

namespace Resyaku.Infrastructure.Authentication.Identity
{
    public sealed class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public ApplicationUser User { get; } = null!;
        public ApplicationRole Role { get; } = null!;
    }
}
