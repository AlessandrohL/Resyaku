using Resyaku.Application.DTOs.ServiceAreas;
using Resyaku.Application.DTOs.Tables;

namespace Resyaku.Web.ViewModels.Tables
{
    public class UpdateTableViewModel
    {
        public int TableId { get; init; }
        public string Name { get; init; } = null!;
        public int MinCapacity { get; init; }
        public int MaxCapacity { get; init; }
        public int SelectedServiceAreaId { get; init; }
        public bool IsActive { get; init; }

        public List<ServiceAreaSummaryDto> AvailableServiceAreas { get; set; } = []; 

        public UpdateTableViewModel() { }

        public UpdateTableViewModel(TableSummaryDto table, List<ServiceAreaSummaryDto> availableServiceAreas)
        {
            TableId = table.Id;
            Name = table.Name;
            MinCapacity = table.MinCapacity;
            MaxCapacity = table.MaxCapacity;
            SelectedServiceAreaId = table.ServiceArea.Id;
            IsActive = table.IsActive;
            AvailableServiceAreas = availableServiceAreas;
        }
    }
}
