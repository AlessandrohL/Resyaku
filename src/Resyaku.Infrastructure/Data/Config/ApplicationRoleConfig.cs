using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resyaku.Infrastructure.Authentication.Identity;

namespace Resyaku.Infrastructure.Data.Config
{
    public sealed class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("ApplicationRole");

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            
            builder
                .Property(p => p.NormalizedName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
