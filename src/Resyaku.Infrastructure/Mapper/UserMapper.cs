using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.DTOs.Users;

namespace Resyaku.Infrastructure.Mapper
{
    public static class UserMapper
    {
        public static UserInfoDto ToUserInfo(this ApplicationUser user)
        {
            return new UserInfoDto(
                user.Id,
                user.UserName!,
                user.Lastname,
                $"{user.Firstname} {user.Lastname ?? string.Empty}",
                user.UserName!,
                user.Email!,
                user.PhoneNumber!,
                user.LockoutEnabled,
                user.UserRoles.Select(r => r.Role.Name!));
        }
    }
}
