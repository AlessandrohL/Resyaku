using MediatR;

namespace Resyaku.Application.Features.ReservationSettings.Queries.GetBookingPreferences
{
    public record GetBookingPreferencesQuery : IRequest<GetBookingPreferencesDto>;
}
