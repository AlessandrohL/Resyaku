using MediatR;

namespace Resyaku.Application.Features.RestaurantSettings.Queries.GetRestaurantPreferences
{
    public record GetRestaurantPreferencesQuery : IRequest<GetRestaurantPreferencesDto>;
}
