namespace Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersDto
    {
        public string UserId { get; init; } = null!;
        public string Firstname { get; init; } = null!;
        public string? Lastname { get; init; }
        public string Fullname { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string PhoneNumber { get; init; } = null!;
        public bool LockoutEnabled { get; init; }
        public IEnumerable<string> Roles { get; init; } = [];
    }

}
