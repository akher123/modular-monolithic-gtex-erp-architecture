using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class ContactBusinessProfileMapping : IEntityTypeConfiguration<ContactBusinessProfile>
    {
        public void Configure(EntityTypeBuilder<ContactBusinessProfile> builder)
        {
            builder.ToTable("ContactBusinessProfile", "core");

            builder.Property(e => e.ContactId)
                .IsRequired();

            builder.Property(e => e.EntityTypeId)
                .IsRequired();

            builder.Property(e => e.BusinessProfileId)
                .IsRequired();

            builder.HasOne(d => d.Contact)
                .WithMany(p => p.ContactBusinessProfiles)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.ContactBusinessProfiles)
                .HasForeignKey(d => d.BusinessProfileId);

            builder.HasOne(d => d.EntityType)
                .WithMany(p => p.ContactBusinessProfiles)
                .HasForeignKey(d => d.EntityTypeId);
        }
    }
}
