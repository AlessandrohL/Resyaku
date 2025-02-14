using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Resyaku.Infrastructure;
using Resyaku.Infrastructure.AppOptions;
using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.Data;
using Resyaku.Infrastructure.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddValidatorsFromAssemblies([
        Resyaku.Application.AssemblyReference.Assembly,
        Resyaku.Infrastructure.AssemblyReference.Assembly,
        Resyaku.Web.AssemblyReference.Assembly
    ]);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.AllowedForNewUsers = false;
})
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = "RYToken";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.LoginPath = "/Auth/Login";
});

builder.Services.Configure<BookingPreferencesOptions>(builder.Configuration.GetRequiredSection("DefaultBookingPreferences"));
builder.Services.Configure<RestaurantPreferencesOptions>(builder.Configuration.GetRequiredSection("DefaultRestaurantPreferences"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    await SeedDefaultAdmin.Initialize(scope.ServiceProvider, configuration);
    await SeedDefaultApplicationPreferences.Initialize(scope.ServiceProvider, configuration);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
