namespace Resyaku.Application.Features.Tables.Queries.GetAvailableTables
{
    public class GetAvailableTablesDto
    {
        public int TableId { get; set; }
        public string TableName { get; set; } = null!;
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public string ServiceAreaName { get; set; } = null!;
    }

}
