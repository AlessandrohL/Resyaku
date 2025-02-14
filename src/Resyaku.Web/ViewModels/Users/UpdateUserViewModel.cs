namespace Resyaku.Web.ViewModels.Users
{
    public class UpdateUserViewModel(
        string userId,
        string firstname,
        string? lastname,
        string phone,
        string email,
        string username,
        bool changePassword,
        string newPassword,
        bool lockoutEnabled,
        IEnumerable<string> roles)
    {
        public string UserId { get; init; } = userId;
        public string Firstname { get; init; } = firstname;
        public string? Lastname { get; init; } = lastname;
        public string Phone { get; init; } = phone;
        public string Email { get; init; } = email;
        public string Username { get; init; } = username;
        public bool ChangePassword { get; init; } = changePassword;
        public string NewPassword { get; init; } = newPassword;
        public bool LockoutEnabled { get; init; } = lockoutEnabled;
        public IEnumerable<string> Roles { get; init; } = roles ?? [];
    }
}
