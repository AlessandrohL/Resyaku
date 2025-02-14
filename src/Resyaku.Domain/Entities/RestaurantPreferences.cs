using Resyaku.Domain.Primitives;

namespace Resyaku.Domain.Entities
{
    public sealed class RestaurantPreferences : IAuditableEntity
    {
        public int RestaurantPrefId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public string RowUlid { get; init; } = null!;
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ModifiedOnUtc { get; set; }

        public bool IsSpecial { get; init; } = false;

        private RestaurantPreferences() { }

        public static RestaurantPreferences Create(
            string name,
            string description,
            TimeSpan openingTime,
            TimeSpan closingTime,
            string rowUlid)
        {
            return new RestaurantPreferences
            {
                Name = name,
                Description = description,
                OpeningTime = openingTime,
                ClosingTime = closingTime,
                RowUlid = rowUlid,
                CreatedOnUtc = DateTime.UtcNow
            };
        }
    }
}
