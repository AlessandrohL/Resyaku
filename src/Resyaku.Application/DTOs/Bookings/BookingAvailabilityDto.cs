namespace Resyaku.Application.DTOs.Bookings;

public record BookingAvailabilityDto(
    DateOnly MinBookingDate,
    DateOnly MaxBookingDate,
    IList<TimeOnly> AvailableBookingTimes, 
    IList<int> AvailableDurations,
    IList<int> AvailablePartySize);
