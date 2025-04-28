using Resyaku.Application.DTOs.ServiceAreas;
using Resyaku.Application.DTOs.Tables;
using Resyaku.Domain.Entities;

namespace Resyaku.Application.Mapper;

public static class TableMapper
{
    public static TableSummaryDto ToTableSummary(this Table table)
    {
        return new TableSummaryDto(
            table.TableId,
            table.Name,
            table.MinCapacity,
            table.MaxCapacity,
            new ServiceAreaSummaryDto(
                table.ServiceArea.ServiceAreaId, 
                table.ServiceArea.Name),
            table.IsActive);
    }
}
