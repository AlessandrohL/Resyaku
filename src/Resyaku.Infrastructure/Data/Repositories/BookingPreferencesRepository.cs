using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Features.ReservationSettings.Services.DTOs;
using Resyaku.Application.Mapper;

namespace Resyaku.Infrastructure.Data.Repositories
{
    public sealed class BookingPreferencesRepository(ApplicationDbContext dbContext) : IBookingPreferencesRepository
    {
        public async Task<GetBookingPreferencesDto?> GetPreferencesAsync()
        {
            return await dbContext.BookingPreferences
                .AsNoTracking()
                .Select(bp => bp.ToBookingPreferencesDto())
                .FirstOrDefaultAsync();
        }
    }
}
