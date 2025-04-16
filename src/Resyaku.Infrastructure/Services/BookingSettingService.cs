using Microsoft.Extensions.Caching.Memory;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Features.ReservationSettings.Services;
using Resyaku.Application.Features.ReservationSettings.Services.DTOs;

namespace Resyaku.Infrastructure.Services
{
    public sealed class BookingSettingService(
        IBookingPreferencesRepository bookingPreferencesRepository, 
        IMemoryCache cache)
        : IBookingSettingsService
    {
        private const string CacheKey = "BookingSettings";
        public async Task<GetBookingPreferencesDto> GetBookingPreferencesAsync(CancellationToken cancellationToken)
        {
            var cachedPreferences = cache.Get<GetBookingPreferencesDto>(CacheKey);

            if (cachedPreferences is not null)
            {
                return cachedPreferences;
            }

            var preferences = await bookingPreferencesRepository.GetPreferencesAsync();

            if (preferences is not null)
            {
                cache.Set(CacheKey, preferences, new MemoryCacheEntryOptions
                {
                    Priority = CacheItemPriority.NeverRemove
                });
            }

            return preferences!;
        }

        public void InvalidateCache()
        {
            cache.Remove(CacheKey);
        }
    }
}
