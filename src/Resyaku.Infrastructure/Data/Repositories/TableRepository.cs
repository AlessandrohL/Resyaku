using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.DTOs.ServiceAreas;
using Resyaku.Application.DTOs.Tables;
using Resyaku.Application.Features.Tables.Queries.GetAllTables;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Enums;
using Resyaku.Domain.Extensions;
using Resyaku.Domain.Primitives;

namespace Resyaku.Infrastructure.Data.Repositories
{
    public sealed class TableRepository(ApplicationDbContext dbContext) : ITableRepository
    {
        public async Task<CollectionResult<TableSummaryDto>> GetAllTablesAsync(
            GetAllTablesQueryParams queryParams)
        {
            var query = dbContext.Tables
                .AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(queryParams.SearchTerm),
                    t => EF.Functions.Like(t.Name, $"%{queryParams.SearchTerm}%"))
                .WhereIf(queryParams.ServiceAreaId != 0,
                    t => t.ServiceAreaId == queryParams.ServiceAreaId);

            int count = await query.CountAsync();
            var tables = await query
                .ApplyOrdering(queryParams)
                .ApplyPagination(queryParams)
                .Select(t => new TableSummaryDto(
                    t.TableId,
                    t.Name,
                    t.MinCapacity,
                    t.MaxCapacity,
                    new ServiceAreaSummaryDto(t.ServiceAreaId, t.ServiceArea.Name),
                    t.IsActive))
                .ToListAsync();

            return new CollectionResult<TableSummaryDto>(tables, count);
        }

        public async Task<IEnumerable<AvailableTableDto>> GetAvailableTablesAsync(
            DateOnly date, 
            TimeOnly startTime, 
            TimeOnly endTime)
        {
            return await dbContext.Tables
                .AsNoTracking()
                .Where(t => !t.Bookings.Any(b =>
                    b.Status != BookingStatus.Cancelled &&
                    b.BookingDate == date &&
                    b.StartTime < endTime &&
                    b.EndTime > startTime))
                .Select(t => new AvailableTableDto(
                    t.TableId,
                    t.Name,
                    t.MinCapacity,
                    t.MaxCapacity,
                    t.ServiceArea.Name))
                .ToListAsync();
        }

        public async Task<bool> AreTablesAvailableAsync(
            IEnumerable<int> tableIds, 
            DateOnly date, 
            TimeOnly startTime, 
            TimeOnly endTime)
        {
            return await dbContext.Tables
                .Where(t => tableIds.Contains(t.TableId))
                .AllAsync(t => !t.Bookings.Any(b =>
                    b.Status != BookingStatus.Cancelled &&
                    b.BookingDate == date &&
                    b.StartTime < endTime &&
                    b.EndTime > startTime));
        }

        public async Task<Table?> GetByIdAsync(int tableId, bool trackChanges = true)
        {
            var query = dbContext.Tables.AsQueryable();
            if (!trackChanges) query = query.AsNoTracking();

            return await query
                .Where(t => t.TableId == tableId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalCapacityAsync(IEnumerable<int> tableIds)
        {
            return await dbContext.Tables
                .Where(t => tableIds.Contains(t.TableId))
                .SumAsync(t => t.MaxCapacity);
        }

        public void Add(Table table)
        {
            dbContext.Tables.Add(table);
        }
    }
}
