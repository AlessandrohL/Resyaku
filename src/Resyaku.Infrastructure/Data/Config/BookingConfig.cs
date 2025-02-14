using Resyaku.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Booking");

            builder.HasKey(p => p.BookingId);

            builder
                .Property(p => p.CustomerId)
                .IsRequired();

            builder
                .Property(p => p.Reference)
                .IsRequired();

            builder
                .HasIndex(p => p.Reference)
                .IsUnique();

            builder
                .Property(p => p.BookingDate)
                .IsRequired();

            builder
                .HasIndex(p => p.BookingDate)
                .IsDescending();

            builder
                .Property(p => p.BookingTime)
                .IsRequired();


            builder
                .Property(p => p.Duration)
                .IsRequired()
                .HasColumnType("smallint");

            builder
                .Property(p => p.EndTime)
                .IsRequired();

            builder
                .Property(p => p.GuestCount)
                .IsRequired()
                .HasColumnType("tinyint");

            builder
                .Property(p => p.Status)
                .IsRequired()
                .HasColumnType("smallint")
                .HasConversion<int>();

            builder.ToTable(t => t.HasCheckConstraint("CK_Booking_Status", "Status IN (1, 2, 3, 4, 5)"));

            builder
                .Property(p => p.PrivateComment)
                .HasColumnName("PrivateComment")
                .IsRequired(false)
                .HasMaxLength(250);

            builder
                .Property(p => p.PublicComment)
                .IsRequired(false)
                .HasMaxLength(250);

            builder
                .Property(p => p.ContactPhone)
                .IsRequired(true)
                .HasMaxLength(9)
                .IsFixedLength();

            builder
                .Property(p => p.IsConfirmed)
                .IsRequired();

            builder
                .Property(p => p.IsWalking)
                .IsRequired();

            builder
                .Property(p => p.CreatedOnUtc)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder
                .HasIndex(p => p.CreatedOnUtc)
                .IsDescending();

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

            builder
                .HasMany(p => p.Tables)
                .WithMany(t => t.Bookings)
                .UsingEntity<BookingTable>();
        }
    }
}
