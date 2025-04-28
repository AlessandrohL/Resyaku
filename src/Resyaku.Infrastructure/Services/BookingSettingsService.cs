using Microsoft.Extensions.Caching.Memory;
using Resyaku.Application.Abstractions.Services;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.BookingSettings;

namespace Resyaku.Infrastructure.Services
{
    public sealed class BookingSettingsService(
        IBookingSettingsRepository bookingSettingsRepository, 
        IMemoryCache cache)
        : IBookingSettingsService
    {
        private const string CacheKey = "BookingSettings";

        public async Task<BookingSettingsInfoDto> RetrieveSettingsAsync()
        {
            var cachedPreferences = cache.Get<BookingSettingsInfoDto>(CacheKey);

            if (cachedPreferences is not null)
            {
                return cachedPreferences;
            }

            var bookingSettings = await bookingSettingsRepository.GetSettingsAsync() 
                ?? throw new Exception("CustomEx");
            
            cache.Set(CacheKey, bookingSettings, new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            });

            return bookingSettings;
        }

        public void InvalidateCache()
        {
            cache.Remove(CacheKey);
        }
    }
}
