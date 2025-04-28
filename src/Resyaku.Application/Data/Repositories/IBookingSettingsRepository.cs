using Resyaku.Application.DTOs.BookingSettings;

namespace Resyaku.Application.Data.Repositories
{
    public interface IBookingSettingsRepository
    {
        Task<BookingSettingsInfoDto?> GetSettingsAsync();
    }
}
