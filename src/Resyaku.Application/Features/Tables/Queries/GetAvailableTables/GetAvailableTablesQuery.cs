using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.Tables;

namespace Resyaku.Application.Features.Tables.Queries.GetAvailableTables
{
    public record GetAvailableTablesQuery(
        DateOnly BookingDate,
        string BookingTime,
        int Duration) : IRequest<List<AvailableTableDto>>;

    public sealed class GetAvailableTablesQueryHandler(ITableRepository tableRepository)
        : IRequestHandler<GetAvailableTablesQuery, List<AvailableTableDto>>
    {
        public async Task<List<AvailableTableDto>> Handle(
            GetAvailableTablesQuery request,
            CancellationToken cancellationToken)
        {
            var bookingStartTime = TimeOnly.Parse(request.BookingTime);
            var bookingEndTime = bookingStartTime.AddMinutes(request.Duration);

            var availableTables = await tableRepository.GetAvailableTablesAsync(
                request.BookingDate, bookingStartTime, bookingEndTime);

            return availableTables.ToList();
        }
    }
}
