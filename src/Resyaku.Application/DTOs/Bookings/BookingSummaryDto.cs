using Resyaku.Domain.Enums;

namespace Resyaku.Application.DTOs.Bookings;

public record BookingSummaryDto(
    int Id,
    string Reference,
    DateTime CreatedAt,
    DateOnly Date,
    TimeOnly StartTime,
    BookingStatus Status,
    int PartySize,
    string[] Tables,
    string CustomerDni,
    string CustomerName);
