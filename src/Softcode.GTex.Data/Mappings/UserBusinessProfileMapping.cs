using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class UserBusinessProfileMapping : IEntityTypeConfiguration<UserBusinessProfile>
    {
        public void Configure(EntityTypeBuilder<UserBusinessProfile> builder)
        {
            builder.ToTable("UserBusinessProfile", "security");

            builder.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.UserBusinessProfiles)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserBusinessProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
