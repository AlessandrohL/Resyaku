using MediatR;
using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data;
using Resyaku.Domain.Enums;

namespace Resyaku.Application.Features.Tables.Queries.GetAvailableTables
{
    public sealed class GetAvailableTablesQueryHandler(IApplicationDbContext dbContext)
        : IRequestHandler<GetAvailableTablesQuery, List<GetAvailableTablesDto>>
    {
        public async Task<List<GetAvailableTablesDto>> Handle(
            GetAvailableTablesQuery request,
            CancellationToken cancellationToken)
        {
            TimeSpan bookingStartTime = TimeSpan.Parse(request.BookingTime);
            var bookingEndDateTime = request.BookingDate
                .Add(bookingStartTime)
                .AddMinutes(request.Duration);

            var availableTables = await dbContext
                .Tables
                .AsNoTracking()
                .Include(t => t.Bookings)
                .Include(t => t.ServiceArea)
                .Where(t => !t.Bookings.Any(b =>
                    b.Status != BookingStatus.Cancelled &&
                    b.BookingDate == request.BookingDate &&
                    b.BookingTime < bookingEndDateTime.TimeOfDay &&
                    b.EndTime.TimeOfDay > bookingStartTime))
                .Select(t => new GetAvailableTablesDto
                {
                    TableId = t.TableId,
                    TableName = t.Name,
                    MinCapacity = t.MinCapacity,
                    MaxCapacity = t.MaxCapacity,
                    ServiceAreaName = t.ServiceArea.Name
                })
                .ToListAsync(CancellationToken.None);

            return availableTables;
        }
    }
}
