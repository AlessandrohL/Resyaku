using MediatR;
using Resyaku.Application.Features.ReservationSettings.Services;

namespace Resyaku.Application.Features.Bookings.Queries.GetBookingAvailability
{
    public sealed class GetBookingAvailabilityQueryHandler(IBookingSettingsService bookingSettingsService)
        : IRequestHandler<GetBookingAvailabilityQuery, BookingAvailabilityDto>
    {
        public async Task<BookingAvailabilityDto> Handle(
            GetBookingAvailabilityQuery request, 
            CancellationToken cancellationToken)
        {
            var bookingPreferences = await bookingSettingsService.GetBookingPreferencesAsync(cancellationToken);

            var openingTime = bookingPreferences!.DailyOpeningTime;
            var closingTime = bookingPreferences.DailyClosingTime;
            var increment = bookingPreferences!.BookingTimeIncrement;

            var availableBookingTimes = new List<TimeSpan>();
            while (openingTime <= closingTime)
            {
                if (openingTime > DateTime.Now.TimeOfDay)
                {
                    availableBookingTimes.Add(openingTime);
                }
                openingTime = openingTime.Add(TimeSpan.FromMinutes(increment));
            }

            int maxDuration = (int)(closingTime - bookingPreferences.DailyOpeningTime).TotalMinutes;
            var availableDurations = Enumerable
                .Range(1, maxDuration / increment)
                .Select(i => i * increment)
                .ToList();

            var availableGuestCounts = Enumerable
                .Range(1, bookingPreferences.MaxGuests)
                .ToList();

            return new BookingAvailabilityDto
            {
                AvailableBookingTimes = availableBookingTimes,
                AvailableDurations = availableDurations,
                AvailableGuestCounts = availableGuestCounts
            };
        }
    }
}
