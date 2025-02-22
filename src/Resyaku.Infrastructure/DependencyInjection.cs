using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resyaku.Application.Data;
using Resyaku.Application.Features.ReservationSettings.Services;
using Resyaku.Infrastructure.Data;
using Resyaku.Infrastructure.Data.Interceptors;
using Resyaku.Infrastructure.Services;

namespace Resyaku.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<SoftDeleteInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                string connectionStr = configuration.GetConnectionString("DevConnection")
                    ?? throw new Exception($"{nameof(connectionStr)} Connection string is null.");

                options.UseSqlServer(connectionStr);
                options.AddInterceptors(
                    sp.GetRequiredService<SoftDeleteInterceptor>());
            });

            services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                Application.AssemblyReference.Assembly,
                AssemblyReference.Assembly));

            services.AddMemoryCache();

            services.AddScoped<IBookingSettingsService, BookingSettingService>();

            return services;
        }
    }
}
