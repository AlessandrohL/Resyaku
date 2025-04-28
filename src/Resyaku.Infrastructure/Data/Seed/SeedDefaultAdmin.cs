using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Resyaku.Infrastructure.AppOptions;
using Resyaku.Infrastructure.Authentication.Identity;

namespace Resyaku.Infrastructure.Data.Seed
{
    public sealed class SeedDefaultAdmin
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var adminOptions = serviceProvider.GetRequiredService<IOptions<DefaultAdminOptions>>().Value;

            string[] roles = ["admin", "user"];

            foreach (string role in roles)
            {
                if (!await roleManager.Roles.AnyAsync(r => r.NormalizedName == role.ToUpper()))
                {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }

            if (await userManager.Users.AnyAsync(u => u.UserName == adminOptions.Username))
            {
                return;
            }

            var admin = new ApplicationUser(
                adminOptions.Firstname,
                adminOptions.Lastname,
                adminOptions.Username,
                adminOptions.Email,
                adminOptions.Phone);

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, adminOptions.Password);

            var creationResult = await userManager.CreateAsync(admin);

            if (!creationResult.Succeeded)
            {
                throw new Exception($"Error al crear el usuario por defecto: {string.Join(", ", creationResult.Errors.Select(e => e.Description))}");
            }

            await userManager.AddToRoleAsync(admin, "admin");
            await userManager.AddClaimAsync(admin, new Claim(nameof(admin.Firstname), admin.Firstname));
        }
    }
}
