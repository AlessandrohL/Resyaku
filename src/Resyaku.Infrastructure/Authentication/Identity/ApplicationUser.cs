using Microsoft.AspNetCore.Identity;

namespace Resyaku.Infrastructure.Authentication.Identity
{
    public sealed class ApplicationUser : IdentityUser<Guid>
    {
        public string Firstname { get; set; } = null!;
        public string? Lastname { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; } = [];
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; set; }
        public Guid RowGuid { get; init; } = Guid.NewGuid();

        public ApplicationUser() { }

        public ApplicationUser(
            string firstname,
            string? lastname,
            string? username,
            string email,
            string phone)
        {
            Firstname = firstname;
            Lastname = lastname;
            PhoneNumber = phone;
            Email = email;
            UserName = string.IsNullOrEmpty(username) ? email : username;
        }

        public string GetFullname()
        {
            return $"{Firstname} {Lastname ?? string.Empty}";
        }
    }
}
