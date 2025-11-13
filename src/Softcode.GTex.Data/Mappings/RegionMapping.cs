
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class RegionMapping : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Region", "core");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.ExternalPartnerId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.RegionCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.RegionName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.RegionCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Region_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.RegionLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_Region_Contact_UpdatedBy");

            builder.HasOne(d => d.Logo)
                .WithMany(p => p.Regions)
                .HasForeignKey(d => d.LogoId)
                .HasConstraintName("FK_Region_Photo");
        }
    }
}
