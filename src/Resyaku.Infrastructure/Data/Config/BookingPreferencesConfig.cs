using Resyaku.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class BookingPreferencesConfig : IEntityTypeConfiguration<BookingPreferences>
    {
        public void Configure(EntityTypeBuilder<BookingPreferences> builder)
        {
            builder.ToTable("BookingPreferences");

            builder.HasKey(bp => bp.BookingPrefId);

            builder.Property(bp => bp.BookingPrefId)
                .HasColumnType("smallint");

            builder.Property(bp => bp.BookingTimeIncrement)
                .HasColumnType("smallint");

            builder.Property(bp => bp.MaxGuests)
                .HasColumnType("smallint");

            builder.Property(bp => bp.MinAdvanceNotice)
                .HasColumnType("smallint");

            builder.Property(bp => bp.MaxDaysInAdvance)
                .HasColumnType("smallint");

            builder.Property(bp => bp.ContactEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.ToTable("BookingPreferences", rp =>
            {
                rp.HasCheckConstraint("CK_BookingPreferences_IsSpecial", "[IsSpecial] = 0");
            });

            builder.Property(rp => rp.IsSpecial)
                .HasDefaultValue(false);

            builder.HasIndex(rp => rp.IsSpecial)
                .IsUnique();
        }
    }
}
