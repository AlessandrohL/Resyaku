namespace Resyaku.Application.Features.Tables.Queries.GetAvailableTables
{
    public sealed class GetAvailableTablesQueryParams
    {
        public DateTime BookingDate { get; set; }
        public string BookingTime { get; set; } = null!;
        public int Duration { get; set; }
    }
}
