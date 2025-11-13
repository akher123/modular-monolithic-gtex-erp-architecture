using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Softcode.GTex.Data.Mappings
{
    public class CostCentreMapping : IEntityTypeConfiguration<CostCentre>
    {
        public void Configure(EntityTypeBuilder<CostCentre> entity)
        {
            entity.ToTable("CostCentre", "core");

            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.CostCentres)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.CostCentreCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CostCentre_Contact_CreatedBy");

            entity.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.CostCentreLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_CostCentre_Contact_UpdatedBy");
        }
    }
}
