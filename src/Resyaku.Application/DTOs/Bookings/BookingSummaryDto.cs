using Resyaku.Domain.Enums;

namespace Resyaku.Application.DTOs.Bookings;

public record BookingSummaryDto(
    int BookingId,
    string BookingReference,
    DateTime CreationDate,
    DateOnly BookingDate,
    BookingStatus BookingStatus,
    string[] Tables,
    string CustomerDni,
    string CustomerName);
