using Resyaku.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class ServiceAreaConfig : IEntityTypeConfiguration<ServiceArea>
    {
        public void Configure(EntityTypeBuilder<ServiceArea> builder)
        {
            builder.ToTable("ServiceArea");

            builder.HasKey(p => p.ServiceAreaId);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasMany(p => p.Tables)
                .WithOne(e => e.ServiceArea)
                .HasForeignKey(e => e.ServiceAreaId)
                .IsRequired();

            builder
                .Property(p => p.CreatedOnUtc)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder
                .Property(p => p.ModifiedOnUtc)
                .IsRequired(false);

            builder
                .Property(p => p.IsDeleted)
                .IsRequired();

            builder
                .Property(p => p.DeletedAt)
                .IsRequired(false);

            builder
                .Property(p => p.RowUlid)
                .IsRequired()
                .HasMaxLength(26)
                .IsFixedLength();

            builder
                .HasIndex(p => p.RowUlid)
                .IsUnique();
        }
    }
}
