using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Resyaku.Domain.Entities;
using Resyaku.Infrastructure.AppOptions;

namespace Resyaku.Infrastructure.Data.Seed
{
    public class SeedDefaultBookingSettings
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var bookingOptions = serviceProvider.GetRequiredService<IOptions<DefaultBookingSettingsOptions>>().Value;

            if (!await context.BookingPreferences.AnyAsync())
            {
                var bookingSettings = new BookingSettings(
                    bookingOptions.BookingTimeIncrement,
                    bookingOptions.DailyOpeningTime,
                    bookingOptions.DailyClosingTime,
                    bookingOptions.MinAdvanceNoticeDays,
                    bookingOptions.MaxAdvanceNoticeDays,
                    bookingOptions.MaxGuests,
                    bookingOptions.ContactEmail);

                context.BookingPreferences.Add(bookingSettings);
            }

            await context.SaveChangesAsync();
        }
    }
}
