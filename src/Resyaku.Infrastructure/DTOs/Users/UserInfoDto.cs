namespace Resyaku.Infrastructure.DTOs.Users
{
    public record UserInfoDto(
        Guid Id,
        string Firstname,
        string? Lastname,
        string Fullname,
        string Username,
        string Email,
        string PhoneNumber,
        bool LockoutEnabled,
        IEnumerable<string> Roles);
}
