using Resyaku.Application.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Resyaku.Application.Features.ReservationSettings.Queries.GetBookingPreferences
{
    public sealed class GetBookingPreferencesQueryHandler(IApplicationDbContext dbContext)
        : IRequestHandler<GetBookingPreferencesQuery, GetBookingPreferencesDto?>
    {
        public async Task<GetBookingPreferencesDto?> Handle(
            GetBookingPreferencesQuery request,
            CancellationToken cancellationToken)
        {
            return await dbContext.BookingPreferences
                .AsNoTracking()
                .Select(bp => new GetBookingPreferencesDto
                {
                    BookingTimeIncrement = bp.BookingTimeIncrement,
                    DailyOpeningTime = bp.DailyOpeningTime,
                    DailyClosingTime = bp.DailyClosingTime,
                    MaxGuests = bp.MaxGuests,
                    MinAdvanceNotice = bp.MinAdvanceNotice,
                    MaxDaysInAdvance = bp.MaxDaysInAdvance,
                    ContactEmail = bp.ContactEmail
                })
                .FirstOrDefaultAsync(CancellationToken.None);
        }
    }
}
