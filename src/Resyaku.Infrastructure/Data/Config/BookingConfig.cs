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
                .Property(p => p.StartTime)
                .IsRequired();

            builder
                .Property(p => p.DurationMinutes)
                .IsRequired();

            builder
                .Property(p => p.EndTime)
                .IsRequired();

            builder
                .Property(p => p.PartySize)
                .IsRequired();

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
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder
                .HasIndex(p => p.CreatedAt)
                .IsDescending();

            builder
                .Property(p => p.UpdatedAt)
                .IsRequired(false);

            builder
                .Property(p => p.IsDeleted)
                .IsRequired();

            builder
                .Property(p => p.DeletedAt)
                .IsRequired(false);

            builder
                .Property(p => p.RowGuid)
                .IsRequired();

            builder
                .HasIndex(p => p.RowGuid)
                .IsUnique();

            builder
                .HasMany(p => p.Tables)
                .WithMany(t => t.Bookings)
                .UsingEntity<BookingTable>();
        }
    }
}
