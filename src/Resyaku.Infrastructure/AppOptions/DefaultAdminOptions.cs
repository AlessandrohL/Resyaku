namespace Resyaku.Infrastructure.AppOptions
{
    public sealed class DefaultAdminOptions
    {
        public string Email { get; init; } = null!;
        public string Firstname { get; init; } = null!;
        public string? Lastname { get; init; }
        public string Phone { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
