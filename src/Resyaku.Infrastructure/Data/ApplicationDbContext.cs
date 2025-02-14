using System.Data.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Resyaku.Application.Data;
using Resyaku.Domain.Audit;
using Resyaku.Domain.Entities;
using Resyaku.Infrastructure.Authentication.Identity;

namespace Resyaku.Infrastructure.Data
{
    public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        IdentityDbContext<
            ApplicationUser,
            ApplicationRole,
            Guid,
            IdentityUserClaim<Guid>,
            ApplicationUserRole,
            IdentityUserLogin<Guid>,
            IdentityRoleClaim<Guid>,
            IdentityUserToken<Guid>
            >(options),
        IApplicationDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            builder.Entity<ApplicationUser>(u =>
            {
                u.HasMany(u => u.UserRoles)
                .WithOne(r => r.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            builder.Entity<ApplicationRole>(u =>
            {
                u.HasMany(u => u.UserRoles)
                .WithOne(r => r.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            });

            builder.Entity<ApplicationUserRole>(entity => { entity.ToTable("ApplicationUserRole"); });
            builder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("ApplicationUserClaim"); });
            builder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("ApplicationUserLogin"); });
            builder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("ApplicationUserToken"); });
            builder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("ApplicationRoleClaim"); });
        }

        public async Task<DbTransaction> BeginTransactionAsync()
        {
            var transaction = await Database.BeginTransactionAsync();

            return transaction.GetDbTransaction();
        }

        public DbSet<RestaurantPreferences> RestaurantPreferences { get => Set<RestaurantPreferences>(); }

        public DbSet<BookingPreferences> BookingPreferences { get => Set<BookingPreferences>(); }

        public DbSet<Customer> Customers { get => Set<Customer>(); }

        public DbSet<Booking> Bookings { get => Set<Booking>(); }

        public DbSet<BookingTable> BookingTables { get => Set<BookingTable>(); }

        public DbSet<ServiceArea> ServiceAreas { get => Set<ServiceArea>(); }

        public DbSet<Table> Tables { get => Set<Table>(); }

        public DbSet<ActivityLog> ActivityLogs { get => Set<ActivityLog>(); }

        public DbSet<ActivityLogType> ActivityLogTypes { get => Set<ActivityLogType>(); }
    }
}
