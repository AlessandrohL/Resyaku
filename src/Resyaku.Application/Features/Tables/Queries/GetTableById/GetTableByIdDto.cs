namespace Resyaku.Application.Features.Tables.Queries.GetTableById
{
    public class GetTableByIdDto
    {
        public int TableId { get; set; }
        public string Name { get; set; } = null!;
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public int ServiceAreaId { get; set; }
        public string ServiceAreaName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
