using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.Bookings;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Bookings.Queries.GetAllBookings
{
    public sealed class GetAllBookingsQueryHandler(IBookingRepository bookingRepository)
        : IRequestHandler<GetAllBookingsQuery, PagedList<GetAllBookingsDto>>
    {
        public async Task<PagedList<GetAllBookingsDto>> Handle(
            GetAllBookingsQuery request,
            CancellationToken cancellationToken)
        {
            var bookingsCollectionResult = await bookingRepository.GetAllBookingsAsync(request.QueryParameters);

            return new PagedList<GetAllBookingsDto>(
                items: bookingsCollectionResult.Items,
                page: request.QueryParameters.Page,
                pageSize: request.QueryParameters.PageSize,
                totalCount: bookingsCollectionResult.TotalCount);
        }
    }
}
