using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resyaku.Infrastructure.Authentication.Identity;

namespace Resyaku.Infrastructure.Extensions;

public static class UserManagerExtensions
{
    public static async Task<bool> IsEmailAlreadyInUseAsync(
        this UserManager<ApplicationUser> userManager, 
        string email)
    {
        return await userManager.Users.AnyAsync(u => u.Email == email);
    }

    public static async Task<bool> IsUsernameAlreadyInUseAsync(
        this UserManager<ApplicationUser> userManager,
        string username)
    {
        string normalizedUsername = username.ToUpper();
        return await userManager.Users.AnyAsync(u => u.NormalizedUserName == normalizedUsername);
    }
}
