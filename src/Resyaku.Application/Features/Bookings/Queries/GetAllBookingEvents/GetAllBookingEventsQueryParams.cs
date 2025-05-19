namespace Resyaku.Application.Features.Bookings.Queries.GetAllBookingEvents
{
    public sealed class GetAllBookingEventsQueryParams
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
