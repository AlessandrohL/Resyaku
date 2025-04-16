using Resyaku.Application.Features.Tables.Queries.GetAllTables;
using Resyaku.Application.Features.Tables.Queries.GetAvailableTables;
using Resyaku.Application.Features.Tables.Queries.GetTableById;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Mapper
{
    public static class TableMapper
    {
        public static GetAllTablesDto ToAllTablesDto(this Table table)
        {
            return new GetAllTablesDto
            {
                TableId = table.TableId,
                Name = table.Name,
                MinCapacity = table.MinCapacity,
                MaxCapacity = table.MaxCapacity,
                ServiceAreaId = table.ServiceAreaId,
                ServiceAreaName = table.ServiceArea.Name,
                IsActive = table.IsActive
            };
        }

        public static GetAvailableTablesDto ToAvailableTablesDto(this Table table)
        {
            return new GetAvailableTablesDto
            {
                TableId = table.TableId,
                TableName = table.Name,
                MinCapacity = table.MinCapacity,
                MaxCapacity = table.MaxCapacity,
                ServiceAreaName = table.ServiceArea.Name
            };
        }

        public static GetTableByIdDto ToTableByIdDto(this Table table)
        {
            return new GetTableByIdDto
            {
                TableId = table.TableId,
                Name = table.Name,
                MinCapacity = table.MinCapacity,
                MaxCapacity = table.MaxCapacity,
                ServiceAreaId = table.ServiceAreaId,
                ServiceAreaName = table.ServiceArea.Name,
                IsActive = table.IsActive
            };
        }
    }
}
