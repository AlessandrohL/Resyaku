using System.Data.Common;
using Resyaku.Domain.Audit;
using Resyaku.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Resyaku.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<RestaurantPreferences> RestaurantPreferences { get; }
        DbSet<BookingPreferences> BookingPreferences { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Booking> Bookings { get; }
        DbSet<BookingTable> BookingTables { get; }
        DbSet<ServiceArea> ServiceAreas { get; }
        DbSet<Table> Tables { get; }
        DbSet<ActivityLog> ActivityLogs { get; }
        DbSet<ActivityLogType> ActivityLogTypes { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<DbTransaction> BeginTransactionAsync();
    }
}
