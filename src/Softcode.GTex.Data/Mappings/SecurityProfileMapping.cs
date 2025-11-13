using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class SecurityProfileMapping : IEntityTypeConfiguration<SecurityProfile>
    {
        public void Configure(EntityTypeBuilder<SecurityProfile> builder)
        {
            builder.ToTable("SecurityProfile", "core");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descriptions).IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.ProfileName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ResetUrlexpiryInHours).HasColumnName("ResetURLExpiryInHours");

            builder.Property(e => e.TimeStamp)                
                .IsRowVersion();

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.SecurityProfiles)
                .HasForeignKey(d => d.BusinessProfileId);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.SecurityProfileCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SecurityProfile_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.SecurityProfileLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_SecurityProfile_Contact_UpdatedBy");

            builder.HasOne(d => d.PasswordCombinationType)
                    .WithMany(p => p.SecurityProfiles)
                    .HasForeignKey(d => d.PasswordCombinationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

}