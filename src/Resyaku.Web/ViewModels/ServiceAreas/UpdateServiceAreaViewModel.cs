using Resyaku.Application.DTOs.ServiceAreas;

namespace Resyaku.Web.ViewModels.ServiceAreas
{
    public class UpdateServiceAreaViewModel
    {
        public int ServiceAreaId { get; init; }
        public string Name { get; init; } = null!;

        public UpdateServiceAreaViewModel() { }

        public UpdateServiceAreaViewModel(ServiceAreaSummaryDto serviceArea)
        {
            ServiceAreaId = serviceArea.Id;
            Name = serviceArea.Name;
        }
    }
}
