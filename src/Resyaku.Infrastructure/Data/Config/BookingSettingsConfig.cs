using Resyaku.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class BookingSettingsConfig : IEntityTypeConfiguration<BookingSettings>
    {
        public void Configure(EntityTypeBuilder<BookingSettings> builder)
        {
            builder.ToTable("BookingSettings");

            builder.HasKey(bp => bp.BookingPrefId);

            builder.Property(bp => bp.BookingTimeIncrement);

            builder.Property(bp => bp.DailyOpeningTime)
                .IsRequired();

            builder.Property(bp => bp.DailyClosingTime)
                .IsRequired();

            builder.Property(bp => bp.MinAdvanceNoticeDays);

            builder.Property(bp => bp.MaxAdvanceNoticeDays);

            builder.Property(bp => bp.ContactEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.ToTable("BookingSettings", rp =>
            {
                rp.HasCheckConstraint("CK_BookingSettings_IsSpecial", "[IsSpecial] = 0");
            });

            builder.Property(rp => rp.IsSpecial)
                .HasDefaultValue(false);

            builder.HasIndex(rp => rp.IsSpecial)
                .IsUnique();
        }
    }
}
