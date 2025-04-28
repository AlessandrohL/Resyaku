using Microsoft.Extensions.DependencyInjection;
using Resyaku.Application.Providers;
using Resyaku.Domain.Abstractions;

namespace Resyaku.Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBookingReferenceProvider, BookingReferenceProvider>();

            return services;
        }
    }
}
