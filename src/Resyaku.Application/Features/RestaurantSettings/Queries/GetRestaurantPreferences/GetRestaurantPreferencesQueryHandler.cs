using MediatR;
using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data;

namespace Resyaku.Application.Features.RestaurantSettings.Queries.GetRestaurantPreferences
{
    public sealed class GetRestaurantPreferencesQueryHandler(IApplicationDbContext dbContext)
        : IRequestHandler<GetRestaurantPreferencesQuery, GetRestaurantPreferencesDto?>
    {
        public async Task<GetRestaurantPreferencesDto?> Handle(
            GetRestaurantPreferencesQuery request,
            CancellationToken cancellationToken)
        {
            return await dbContext
                .RestaurantPreferences
                .AsNoTracking()
                .Select(rp => new GetRestaurantPreferencesDto
                {
                    Name = rp.Name,
                    OpeningTime = rp.OpeningTime,
                    ClosingTime = rp.ClosingTime
                })
                .FirstOrDefaultAsync(CancellationToken.None);
        }
    }
}
