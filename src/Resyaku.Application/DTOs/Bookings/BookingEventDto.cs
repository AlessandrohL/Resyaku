namespace Resyaku.Application.DTOs.Bookings
{
    public sealed record BookingEventDto
    {
        public required string Reference { get; init; }
        public required int PartySize { get; init; }
        public string[] Tables { get; init; } = [];
        public required DateOnly Date { get; init; }
        public required TimeOnly StartTime { get; init; }
        public required TimeOnly EndTime { get; init; }
        public required string CustomerName { get; init; }
        public required string CustomerEmail { get; init; }
        public required string CustomerDni { get; init; }
    }
}
