using Resyaku.Infrastructure.DTOs.Roles;
using Resyaku.Infrastructure.DTOs.Users;

namespace Resyaku.Web.ViewModels.Users
{
    public sealed class UpdateUserViewModel
    {
        public string UserId { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string? Lastname { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool ChangePassword { get; set; }
        public string? NewPassword { get; set; }
        public bool LockoutEnabled { get; set; }
        public List<string> SelectedRoles { get; set; } = [];

        public List<RoleSummaryDto> AvailableRoles { get; set; } = [];

        public UpdateUserViewModel() { }

        public UpdateUserViewModel(UserInfoDto userInfo, List<RoleSummaryDto> availableRoles)
        {
            UserId = userInfo.Id.ToString();
            Firstname = userInfo.Firstname;
            Lastname = userInfo.Lastname;
            Phone = userInfo.PhoneNumber;
            Email = userInfo.Email;
            Username = userInfo.Username;
            LockoutEnabled = userInfo.LockoutEnabled;
            SelectedRoles = userInfo.Roles.ToList();
            AvailableRoles = availableRoles;
        }
    }
}
