using Resyaku.Application.DTOs.Tables;
using Resyaku.Application.Features.Tables.Queries.GetAllTables;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Data.Repositories
{
    public interface ITableRepository
    {
        Task<CollectionResult<TableSummaryDto>> GetAllTablesAsync(GetAllTablesQueryParams queryParams);
        Task<IEnumerable<AvailableTableDto>> GetAvailableTablesAsync(
            DateOnly date,
            TimeOnly startTime,
            TimeOnly endTime);
        Task<bool> AreTablesAvailableAsync(
            IEnumerable<int> tableIds,
            DateOnly date,
            TimeOnly startTime,
            TimeOnly endTime);
        Task<Table?> GetByIdAsync(int tableId, bool trackChanges = true);
        Task<int> GetTotalCapacityAsync(IEnumerable<int> tableIds);
        void Add(Table table);
    }
}
