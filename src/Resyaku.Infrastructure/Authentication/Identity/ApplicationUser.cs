using Microsoft.AspNetCore.Identity;

namespace Resyaku.Infrastructure.Authentication.Identity
{
    public sealed class ApplicationUser : IdentityUser<Guid>
    {
        public string Firstname { get; set; } = null!;
        public string? Lastname { get; set; }
        public string RowUlid { get; private set; } = null!;
        public ICollection<ApplicationUserRole> UserRoles { get; } = [];
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ModifiedOnUtc { get; set; }

        private ApplicationUser() { }

        public static ApplicationUser Create(
            string firstname,
            string? lastname,
            string phone,
            string email,
            string? username)
        {
            var user = new ApplicationUser
            {
                Firstname = firstname,
                Lastname = lastname,
                PhoneNumber = phone,
                Email = email,
                UserName = string.IsNullOrEmpty(username) ? email : username,
                RowUlid = Ulid.NewUlid().ToString(),
                CreatedOnUtc = DateTime.UtcNow
            };

            return user;
        }

        public string GetFullname()
        {
            return $"{Firstname} {Lastname ?? string.Empty}";
        }
    }
}
