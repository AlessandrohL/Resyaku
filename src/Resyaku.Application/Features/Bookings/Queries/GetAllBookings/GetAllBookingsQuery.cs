using MediatR;
using Resyaku.Application.Features.Bookings.Queries.GetBookings;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Features.Bookings.Queries.GetAllBookings
{
    public record GetAllBookingsQuery(GetAllBookingsQueryParams QueryParameters)
        : IRequest<PagedList<GetAllBookingsDto>>;
}
