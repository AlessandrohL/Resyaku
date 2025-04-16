using Resyaku.Application.Features.Tables.Queries.GetAllTables;
using Resyaku.Application.Features.Tables.Queries.GetAvailableTables;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Data.Repositories
{
    public interface ITableRepository
    {
        Task<CollectionResult<GetAllTablesDto>> GetAllTablesAsync(GetAllTablesQueryParameters queryParams);
        Task<IEnumerable<GetAvailableTablesDto>> GetAvailableTablesAsync(
            DateTime bookingDate,
            TimeSpan bookingStartTime,
            TimeSpan bookingEndTime);
        Task<bool> AreTablesAvailableAsync(
            IEnumerable<int> tableIds,
            DateTime bookingDate,
            TimeSpan startTime,
            int duration);
        Task<Table?> GetByIdAsync(int tableId, bool trackChanges = true);
        Task<int> GetTotalCapacityAsync(IEnumerable<int> tableIds);
        void Add(Table table);
    }
}
