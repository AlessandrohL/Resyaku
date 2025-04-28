using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resyaku.Infrastructure.Authentication.Identity;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUser");

            builder
                .Property(p => p.Firstname)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Lastname)
                .IsRequired(false)
                .HasMaxLength(50);

            builder
                .Property(p => p.UserName)
                .IsRequired();

            builder
                .Property(p => p.NormalizedUserName)
                .IsRequired();
            
            builder
                .Property(p => p.Email)
                .IsRequired();
            
            builder
                .Property(p => p.NormalizedEmail)
                .IsRequired();

            builder
                .Property(p => p.PasswordHash)
                .IsRequired();

            builder
                .Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(9)
                .IsFixedLength();

            builder
                .Property(p => p.PhoneNumberConfirmed)
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
