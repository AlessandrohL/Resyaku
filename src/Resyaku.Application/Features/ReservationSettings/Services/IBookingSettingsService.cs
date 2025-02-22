using Resyaku.Application.Features.ReservationSettings.Services.DTOs;

namespace Resyaku.Application.Features.ReservationSettings.Services
{
    public interface IBookingSettingsService
    {
        Task<GetBookingPreferencesDto> GetBookingPreferencesAsync(CancellationToken cancellationToken);
        void InvalidateCache();
    }
}
