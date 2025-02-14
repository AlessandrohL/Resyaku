namespace Resyaku.Infrastructure.AppOptions
{
    public class RestaurantPreferencesOptions
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
    }
}
