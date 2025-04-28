using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.BookingSettings;

namespace Resyaku.Infrastructure.Data.Repositories
{
    public sealed class BookingPreferencesRepository(ApplicationDbContext dbContext) : IBookingSettingsRepository
    {
        public async Task<BookingSettingsInfoDto?> GetSettingsAsync()
        {
            return await dbContext.BookingPreferences
                .AsNoTracking()
                .Select(bp => new BookingSettingsInfoDto(
                    bp.BookingTimeIncrement,
                    bp.DailyOpeningTime,
                    bp.DailyClosingTime,
                    bp.MinAdvanceNoticeDays,
                    bp.MaxAdvanceNoticeDays,
                    bp.MaxPartySize,
                    bp.ContactEmail))
                .FirstOrDefaultAsync();
        }
    }
}
