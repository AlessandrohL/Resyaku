using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Resyaku.Domain.Primitives;

namespace Resyaku.Infrastructure.Data.Interceptors
{
    public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<IAuditableEntity>> entries = eventData
                .Context
                .ChangeTracker
                .Entries<IAuditableEntity>();

            foreach (var entry in entries)
            {
                // EntityState.Added

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedOnUtc = DateTime.UtcNow;
                }

                // EntityState.Deleted
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
