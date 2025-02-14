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
                .Property(p => p.CustomerId)
                .IsRequired();

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder
                .Property(p => p.Lastname)
                .IsRequired()
                .HasMaxLength(40);

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
