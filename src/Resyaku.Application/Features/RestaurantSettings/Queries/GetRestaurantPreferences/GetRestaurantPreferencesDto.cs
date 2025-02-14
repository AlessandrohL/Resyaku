namespace Resyaku.Application.Features.RestaurantSettings.Queries.GetRestaurantPreferences
{
    public class GetRestaurantPreferencesDto
    {
        public string Name { get; set; } = null!;
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
    }
}
