using Resyaku.Application.Features.ReservationSettings.Services.DTOs;

namespace Resyaku.Application.Data.Repositories
{
    public interface IBookingPreferencesRepository
    {
        Task<GetBookingPreferencesDto?> GetPreferencesAsync();
    }
}
