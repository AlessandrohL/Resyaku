using Resyaku.Domain.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class ActivityLogTypeConfig : IEntityTypeConfiguration<ActivityLogType>
    {
        public void Configure(EntityTypeBuilder<ActivityLogType> builder)
        {
            builder.ToTable("ActivityLogType");

            builder.HasKey(p => p.ActivityLogTypeId);

            builder
                .Property(p => p.Action)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasMany(p => p.ActivityLogs)
                .WithOne(e => e.ActivityLogType)
                .HasForeignKey(e => e.ActivityLogTypeId)
                .IsRequired();
        }
    }
}
