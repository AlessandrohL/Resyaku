using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Resyaku.Domain.Entities;
using Resyaku.Infrastructure.AppOptions;

namespace Resyaku.Infrastructure.Data.Seed
{
    public class SeedDefaultApplicationPreferences
    {
        public static async Task Initialize(
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var bookingOptions = serviceProvider.GetRequiredService<IOptions<BookingPreferencesOptions>>().Value;

            if (!await context.BookingPreferences.AnyAsync(CancellationToken.None))
            {
                var bookingPreferences = BookingPreferences.Create(
                    bookingTimeIncrement: bookingOptions.BookingTimeIncrement,
                    dailyOpeningTime: bookingOptions.DailyOpeningTime,
                    dailyClosingTime: bookingOptions.DailyClosingTime,
                    maxGuests: bookingOptions.MaxGuests,
                    minAdvanceNotice: bookingOptions.MinAdvanceNotice,
                    maxDaysInAdvance: bookingOptions.MaxDaysInAdvance,
                    contactEmail: bookingOptions.ContactEmail,
                    rowUlid: Ulid.NewUlid().ToString());

                context.BookingPreferences.Add(bookingPreferences);
            }

            await context.SaveChangesAsync();
        }
    }
}
