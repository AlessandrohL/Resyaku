using MediatR;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.Bookings;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Bookings.Queries.GetAllBookings
{
    public record GetAllBookingsQuery(GetAllBookingsQueryParams QueryParameters)
        : IRequest<PagedList<BookingSummaryDto>>;

    public sealed class GetAllBookingsQueryHandler(IBookingRepository bookingRepository)
        : IRequestHandler<GetAllBookingsQuery, PagedList<BookingSummaryDto>>
    {
        public async Task<PagedList<BookingSummaryDto>> Handle(
            GetAllBookingsQuery request,
            CancellationToken cancellationToken)
        {
            var bookingsCollectionResult = await bookingRepository.GetAllBookingsAsync(request.QueryParameters);

            return new PagedList<BookingSummaryDto>(
                items: bookingsCollectionResult.Items,
                page: request.QueryParameters.Page,
                pageSize: request.QueryParameters.PageSize,
                totalCount: bookingsCollectionResult.TotalCount);
        }
    }

}
