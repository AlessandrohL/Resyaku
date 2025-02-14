using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resyaku.Infrastructure.Authentication.Identity;

namespace Resyaku.Infrastructure.Data.Seed
{
    public sealed class SeedDefaultAdmin
    {
        public static async Task Initialize(
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            string[] roles = ["admin", "user"];

            foreach (string role in roles)
            {
                if (await roleManager.FindByNameAsync(role) is null)
                {
                    await roleManager.CreateAsync(ApplicationRole.Create(role));
                }
            }

            string defaultAdminUsername = configuration["DefaultAdmin:Username"]!;
            string defaultAdminEmail = configuration["DefaultAdmin:Email"]!;
            string defaultAdminPassword = configuration["DefaultAdmin:Password"]!;

            if (await userManager.Users.AnyAsync(u => u.UserName == defaultAdminUsername))
            {
                return;
            }

            var admin = ApplicationUser.Create(
                    firstname: configuration["DefaultAdmin:Firstname"]!,
                    lastname: configuration["DefaultAdmin:Lastname"] ?? string.Empty,
                    phone: configuration["DefaultAdmin:Phone"]!,
                    email: defaultAdminEmail!,
                    username: defaultAdminUsername);

            var creationResult = await userManager.CreateAsync(admin, configuration["DefaultAdmin:Password"]!);

            if (!creationResult.Succeeded)
            {
                throw new Exception($"Error al crear el usuario por defecto: {string.Join(", ", creationResult.Errors.Select(e => e.Description))}");
            }

            await userManager.AddToRoleAsync(admin, "admin");
            await userManager.AddClaimAsync(admin, new Claim(nameof(admin.Firstname), admin.Firstname));
        }
    }
}
