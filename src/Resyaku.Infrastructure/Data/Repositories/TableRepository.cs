using Microsoft.EntityFrameworkCore;
using Resyaku.Application.Data.Repositories;
using Resyaku.Application.Features.Tables.Queries.GetAllTables;
using Resyaku.Application.Features.Tables.Queries.GetAvailableTables;
using Resyaku.Application.Features.Tables.Queries.GetTableById;
using Resyaku.Application.Mapper;
using Resyaku.Domain.Entities;
using Resyaku.Domain.Enums;
using Resyaku.Domain.Extensions;
using Resyaku.Domain.Primitives;

namespace Resyaku.Infrastructure.Data.Repositories
{
    public sealed class TableRepository(ApplicationDbContext dbContext) : ITableRepository
    {
        public async Task<CollectionResult<GetAllTablesDto>> GetAllTablesAsync(
            GetAllTablesQueryParameters queryParams)
        {
            var query = dbContext.Tables
                .AsNoTracking()
                //.Include(t => t.ServiceArea)
                .WhereIf(!string.IsNullOrWhiteSpace(queryParams.SearchTerm),
                    t => EF.Functions.Like(t.Name, $"%{queryParams.SearchTerm}%"))
                .WhereIf(queryParams.ServiceAreaId != 0,
                    t => t.ServiceAreaId == queryParams.ServiceAreaId);

            int count = await query.CountAsync();
            var tables = await query
                .ApplyOrdering(queryParams)
                .ApplyPagination(queryParams)
                .Select(t => t.ToAllTablesDto())
                .ToListAsync();

            return new CollectionResult<GetAllTablesDto>(tables, count);
        }

        public async Task<IEnumerable<GetAvailableTablesDto>> GetAvailableTablesAsync(
            DateTime bookingDate, 
            TimeSpan bookingStartTime, 
            TimeSpan bookingEndTime)
        {
            return await dbContext.Tables
                .AsNoTracking()
                .Where(t => t.Bookings.Any(b =>
                    b.Status != BookingStatus.Cancelled &&
                    b.BookingDate == bookingDate &&
                    b.BookingTime < bookingEndTime &&
                    b.EndTime.TimeOfDay > bookingStartTime))
                .Select(t => t.ToAvailableTablesDto())
                .ToListAsync();
        }

        public async Task<bool> AreTablesAvailableAsync(
            IEnumerable<int> tableIds, 
            DateTime bookingDate, 
            TimeSpan startTime, 
            int duration)
        {
            var bookingEndDateTime = bookingDate.Add(startTime).AddMinutes(duration);

            return await dbContext.Tables
                .Where(t => tableIds.Contains(t.TableId))
                .AllAsync(t => !t.Bookings.Any(b =>
                    b.Status != BookingStatus.Cancelled &&
                    b.BookingDate == bookingDate &&
                    b.BookingTime < bookingEndDateTime.TimeOfDay &&
                    b.EndTime.TimeOfDay > startTime));
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
