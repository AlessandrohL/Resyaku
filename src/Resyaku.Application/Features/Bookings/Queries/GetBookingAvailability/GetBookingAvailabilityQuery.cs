using MediatR;
using Resyaku.Application.Abstractions.Services;
using Resyaku.Application.DTOs.Bookings;

namespace Resyaku.Application.Features.Bookings.Queries.GetBookingAvailability
{
    public record GetBookingAvailabilityQuery : IRequest<BookingAvailabilityDto>;

    public sealed class GetBookingAvailabilityQueryHandler(IBookingSettingsService bookingSettingsService)
        : IRequestHandler<GetBookingAvailabilityQuery, BookingAvailabilityDto>
    {
        public async Task<BookingAvailabilityDto> Handle(
            GetBookingAvailabilityQuery request,
            CancellationToken cancellationToken)
        {
            var bookingSettings = await bookingSettingsService.RetrieveSettingsAsync();

            if (bookingSettings is null)
            {
                // TODO: Logging error.
                throw new Exception("SomethingException");
            }

            var openingTime = bookingSettings.DailyOpeningTime;
            var closingTime = bookingSettings.DailyClosingTime;
            var increment = bookingSettings.BookingTimeIncrement;

            var availableBookingTimes = new List<TimeOnly>();

            while (openingTime <= closingTime)
            {
                availableBookingTimes.Add(openingTime);
                openingTime = openingTime.AddMinutes(increment);
            }

            int maxDuration = (int)(closingTime - bookingSettings.DailyOpeningTime).TotalMinutes;

            var availableDurations = Enumerable
                .Range(1, maxDuration / increment)
                .Select(i => i * increment)
                .ToList();

            var availablePartySize = Enumerable
                .Range(1, bookingSettings.MaxPartySize)
                .ToList();

            DateOnly minBookingDate = DateOnly
                .FromDateTime(DateTime.Now.AddDays(bookingSettings.MinAdvanceNoticeDays));

            DateOnly maxBookingDate = DateOnly
                .FromDateTime(DateTime.Now.AddDays(bookingSettings.MaxAdvanceNoticeDays));

            return new BookingAvailabilityDto(
                minBookingDate,
                maxBookingDate,
                availableBookingTimes, 
                availableDurations, 
                availablePartySize);
        }
    }

}
