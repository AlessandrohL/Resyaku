namespace Resyaku.Web.ViewModels.Tables
{
    public class UpdateTableViewModel(
        int tableId, 
        string name, 
        int minCapacity, 
        int maxCapacity, 
        int serviceAreaId, 
        bool isActive)
    {
        public int TableId { get; init; } = tableId;
        public string Name { get; init; } = name;
        public int MinCapacity { get; init; } = minCapacity;
        public int MaxCapacity { get; init; } = maxCapacity;
        public int ServiceAreaId { get; init; } = serviceAreaId;
        public bool IsActive { get; init; } = isActive;
    }
}
