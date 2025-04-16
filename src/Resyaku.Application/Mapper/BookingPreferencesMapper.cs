using Resyaku.Application.Features.ReservationSettings.Services.DTOs;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Mapper
{
    public static class BookingPreferencesMapper
    {
        public static GetBookingPreferencesDto ToBookingPreferencesDto(this BookingPreferences preferences)
        {
            return new GetBookingPreferencesDto
            {
                BookingTimeIncrement = preferences.BookingTimeIncrement,
                DailyOpeningTime = preferences.DailyOpeningTime,
                DailyClosingTime = preferences.DailyClosingTime,
                MaxGuests = preferences.MaxGuests,
                MinAdvanceNotice = preferences.MinAdvanceNotice,
                MaxDaysInAdvance = preferences.MaxDaysInAdvance,
                ContactEmail = preferences.ContactEmail
            };
        }
    }
}
