using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resyaku.Domain.Entities;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(p => p.CustomerId);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Lastname)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .HasIndex(p => new { p.Name, p.Lastname });

            builder
                .Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(9)
                .IsFixedLength();
            
            builder
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasIndex(p => p.Email)
                .IsUnique();
           
            builder
                .Property(p => p.Dni)
                .IsRequired()
                .HasMaxLength(8)
                .IsFixedLength();

            builder
               .HasIndex(p => p.Dni)
               .IsUnique();

            builder
                .HasMany(p => p.Bookings)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .IsRequired();

            builder
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder
                .Property(p => p.UpdatedAt)
                .IsRequired(false);

            builder
                .Property(p => p.IsDeleted)
                .IsRequired();

            builder
                .Property(p => p.RowGuid)
                .IsRequired();

            builder
                .HasIndex(p => p.RowGuid)
                .IsUnique();
        }
    }
}
