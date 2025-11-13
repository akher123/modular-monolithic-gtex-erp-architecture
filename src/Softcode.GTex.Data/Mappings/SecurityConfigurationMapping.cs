
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class SecurityConfigurationMapping : IEntityTypeConfiguration<SecurityConfiguration>
    {
        public void Configure(EntityTypeBuilder<SecurityConfiguration> builder)
        {
            builder.ToTable("SecurityConfiguration", "core");

            builder.Property(e => e.AppHelpContentUrl)
                .HasColumnName("AppHelpContentURL")
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.ApplicationTitle).HasMaxLength(200);

            builder.Property(e => e.B2busernameType).HasColumnName("B2BUsernameType");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EnableRetrievePassword)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.EnableSessionLog)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.EnableSso).HasColumnName("EnableSSO");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.MaximumImageUploadSizeInKb).HasColumnName("MaximumImageUploadSizeInKB");

            builder.Property(e => e.MaximumSesssionSpaceInKb).HasColumnName("MaximumSesssionSpaceInKB");

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.SecurityConfigurations)
                .HasForeignKey(d => d.BusinessProfileId);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.SecurityConfigurationCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SecurityConfiguration_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.SecurityConfigurationLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_SecurityConfiguration_Contact_UpdatedBy");
        }
    }

}
