using Resyaku.Application.DTOs.ServiceAreas;

namespace Resyaku.Web.ViewModels.Tables
{
    public sealed class CreateTableViewModel
    {
        public string Name { get; init; } = null!;
        public int MinCapacity { get; init; }
        public int MaxCapacity { get; init; }
        public int ServiceAreaId { get; init; }
        public bool IsActive { get; init; }
        public List<ServiceAreaSummaryDto> AvailableServiceAreas { get; set; } = [];

        public CreateTableViewModel() { }

        public CreateTableViewModel(List<ServiceAreaSummaryDto> availableServiceAreas)
        {
            AvailableServiceAreas = availableServiceAreas;
        }
    }
}
