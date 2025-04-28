using Resyaku.Application.DTOs.BookingSettings;

namespace Resyaku.Application.Abstractions.Services
{
    public interface IBookingSettingsService
    {
        Task<BookingSettingsInfoDto> RetrieveSettingsAsync();
        void InvalidateCache();
    }
}
