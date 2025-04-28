using Resyaku.Domain.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class ActivityLogConfig : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLog");

            builder.HasKey(p => p.ActivityLogId);

            builder
                .Property(p => p.Message)
                .IsRequired()
                .HasMaxLength(2000);

            builder
                .Property(p => p.ReferenceRowGuid)
                .IsRequired()
                .HasMaxLength(36)
                .IsFixedLength();

            builder
                .Property(p => p.UserId)
                .IsRequired()
                .HasMaxLength(36)
                .IsFixedLength();

            builder
                .Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
