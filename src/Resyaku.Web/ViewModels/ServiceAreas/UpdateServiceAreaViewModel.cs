namespace Resyaku.Web.ViewModels.ServiceAreas
{
    public class UpdateServiceAreaViewModel(int serviceAreaId, string name)
    {
        public int ServiceAreaId { get; init; } = serviceAreaId;
        public string Name { get; init; } = name;
    }
}
