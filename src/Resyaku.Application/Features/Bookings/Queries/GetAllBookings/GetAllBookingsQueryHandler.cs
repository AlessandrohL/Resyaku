using MediatR;
using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data;
using Resyaku.Domain.Extensions;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Bookings.Queries.GetAllBookings
{
    public sealed class GetAllBookingsQueryHandler(IApplicationDbContext dbContext)
        : IRequestHandler<GetAllBookingsQuery, PagedList<GetAllBookingsDto>>
    {
        public async Task<PagedList<GetAllBookingsDto>> Handle(
            GetAllBookingsQuery request,
            CancellationToken cancellationToken)
        {
            var parameters = request.QueryParameters;
            var query = dbContext.Bookings
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Tables)
                .WhereIf(!string.IsNullOrWhiteSpace(parameters.CustomerName), b => EF.Functions.Like(b.Customer.Name, $"%{parameters.CustomerName}%"))
                .WhereIf(!string.IsNullOrWhiteSpace(parameters.CustomerPhone), b => EF.Functions.Like(b.Customer.Phone, $"%{parameters.CustomerPhone}%"))
                .WhereIf(!string.IsNullOrWhiteSpace(parameters.CustomerEmail), b => EF.Functions.Like(b.Customer.Email, $"%{parameters.CustomerEmail}%"))
                .WhereIf(!string.IsNullOrWhiteSpace(parameters.CustomerDni), b => EF.Functions.Like(b.Customer.Dni, $"%{parameters.CustomerDni}%"))
                .WhereIf(parameters.StartDate.HasValue, b => b.BookingDate >= parameters.StartDate!.Value)
                .WhereIf(parameters.EndDate.HasValue, b => b.BookingDate <= parameters.EndDate!.Value);

            int count = await query.CountAsync(CancellationToken.None);

            List<GetAllBookingsDto> bookings = await query
                
                .ApplyOrdering(parameters)
                .ApplyPagination(parameters)
                .Select(b => new GetAllBookingsDto
                {
                    BookingId = b.BookingId,
                    BookingReference = b.Reference,
                    BookingDate = b.BookingDate.Add(b.BookingTime),
                    BookingStatus = b.Status,
                    Tables = b.Tables.Select(t =>  t.Name).ToArray(),
                    CustomerName = b.Customer.Name,
                    CustomerDni = b.Customer.Dni,
                    CreationDate = b.CreatedOnUtc
                })
                .ToListAsync(CancellationToken.None);

            return new PagedList<GetAllBookingsDto>(
                items: bookings,
                page: parameters.Page,
                pageSize: parameters.PageSize,
                totalCount: count);
        }
    }
}
