using MediatR;

namespace Resyaku.Application.Features.Tables.Queries.GetAvailableTables
{
    public record GetAvailableTablesQuery(
        DateTime BookingDate,
        string BookingTime,
        int Duration) : IRequest<List<GetAvailableTablesDto>>;
}
