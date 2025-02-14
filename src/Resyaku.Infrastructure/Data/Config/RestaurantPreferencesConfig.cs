using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resyaku.Domain.Entities;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class RestaurantPreferencesConfig : IEntityTypeConfiguration<RestaurantPreferences>
    {
        public void Configure(EntityTypeBuilder<RestaurantPreferences> builder)
        {
            builder.HasKey(rp => rp.RestaurantPrefId);

            builder.Property(rp => rp.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(rp => rp.Description)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(e => e.OpeningTime)
            .IsRequired();

            builder.Property(e => e.ClosingTime)
                .IsRequired();

            builder.Property(rp => rp.RowUlid)
                .IsRequired()
                .HasMaxLength(26);

            builder.ToTable("RestaurantPreferences", rp =>
            {
                rp.HasCheckConstraint("CK_RestaurantPreferences_IsSpecial", "[IsSpecial] = 0");
            });

            builder.Property(rp => rp.IsSpecial)
                .HasDefaultValue(false);

            builder.HasIndex(rp => rp.IsSpecial)
                .IsUnique();
        }
    }
}
