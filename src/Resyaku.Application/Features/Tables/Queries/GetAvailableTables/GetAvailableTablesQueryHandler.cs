using MediatR;
using Resyaku.Application.Data.Repositories;

namespace Resyaku.Application.Features.Tables.Queries.GetAvailableTables
{
    public sealed class GetAvailableTablesQueryHandler(ITableRepository tableRepository)
        : IRequestHandler<GetAvailableTablesQuery, List<GetAvailableTablesDto>>
    {
        public async Task<List<GetAvailableTablesDto>> Handle(
            GetAvailableTablesQuery request,
            CancellationToken cancellationToken)
        {
            TimeSpan bookingStartTime = TimeSpan.Parse(request.BookingTime);
            TimeSpan bookingEndTime = request.BookingDate
                .Add(bookingStartTime)
                .AddMinutes(request.Duration)
                .TimeOfDay;

            var availableTables = await tableRepository.GetAvailableTablesAsync(
                request.BookingDate, bookingStartTime, bookingEndTime);

            return availableTables.ToList();
        }
    }
}
