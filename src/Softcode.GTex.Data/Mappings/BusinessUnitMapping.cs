using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class BusinessUnitMapping : IEntityTypeConfiguration<BusinessUnit>
    {
        public void Configure(EntityTypeBuilder<BusinessUnit> entity)
        {
            entity.ToTable("BusinessUnit", "core");

            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Desciption)
                .HasMaxLength(400)
                .IsUnicode(false);

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.BusinessUnits)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.BusinessUnitCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessUnit_Contact_CreatedBy");

            entity.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.BusinessUnitLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_BusinessUnit_Contact_UpdatedBy");
        }
    }
}
