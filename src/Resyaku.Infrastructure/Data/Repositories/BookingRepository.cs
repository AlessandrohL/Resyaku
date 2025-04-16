using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.Bookings;
using Resyaku.Application.Features.Bookings.Queries.GetBookings;
using Resyaku.Application.Mapper;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Extensions;
using Resyaku.Domain.Primitives;

namespace Resyaku.Infrastructure.Data.Repositories
{
    public sealed class BookingRepository(ApplicationDbContext dbContext) : IBookingRepository
    {
        public async Task<CollectionResult<GetAllBookingsDto>> GetAllBookingsAsync(GetAllBookingsQueryParams queryParams)
        {
            var query = dbContext.Bookings
                .AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(queryParams.CustomerName), 
                    b => EF.Functions.Like(b.Customer.Name, $"%{queryParams.CustomerName}%"))
                .WhereIf(!string.IsNullOrWhiteSpace(queryParams.CustomerPhone), 
                    b => EF.Functions.Like(b.Customer.Phone, $"%{queryParams.CustomerPhone}%"))
                .WhereIf(!string.IsNullOrWhiteSpace(queryParams.CustomerEmail), 
                    b => EF.Functions.Like(b.Customer.Email, $"%{queryParams.CustomerEmail}%"))
                .WhereIf(!string.IsNullOrWhiteSpace(queryParams.CustomerDni), 
                    b => EF.Functions.Like(b.Customer.Dni, $"%{queryParams.CustomerDni}%"))
                .WhereIf(queryParams.StartDate.HasValue, b => b.BookingDate >= queryParams.StartDate!.Value)
                .WhereIf(queryParams.EndDate.HasValue, b => b.BookingDate <= queryParams.EndDate!.Value);

            int count = await query.CountAsync();
            var bookings = await query
                .ApplyOrdering(queryParams)
                .ApplyPagination(queryParams)
                .Select(b => b.ToGetAllBookingsDto())
                .ToListAsync();

            return new CollectionResult<GetAllBookingsDto>(bookings, count);
        }

        public void Add(Booking booking)
        {
            dbContext.Bookings.Add(booking);
        }

        public void Remove(Booking booking)
        {
            dbContext.Bookings.Remove(booking);
        }
    }
}
