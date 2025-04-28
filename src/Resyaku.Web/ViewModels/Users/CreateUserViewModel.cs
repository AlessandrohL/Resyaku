using Resyaku.Infrastructure.DTOs.Roles;

namespace Resyaku.Web.ViewModels.Users
{
    public sealed class CreateUserViewModel
    {
        public string Firstname { get; set; } = null!;
        public string? Lastname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<string> SelectedRoles { get; set; } = [];

        public List<RoleSummaryDto> AvailableRoles { get; set; } = [];

        public CreateUserViewModel() { }

        public CreateUserViewModel(List<RoleSummaryDto> availableRoles)
        {
            AvailableRoles = availableRoles;
        }
    }
}
