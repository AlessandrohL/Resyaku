using Resyaku.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class TableConfig : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Table");

            builder.HasKey(p => p.TableId);

            builder
                .Property(p => p.TableId)
                .HasColumnType("smallint");

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(p => p.MinCapacity)
                .IsRequired();

            builder
                .Property(p => p.MaxCapacity)
                .IsRequired();

            builder
                .Property(p => p.IsActive)
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
