using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class PostalCodeMapping : IEntityTypeConfiguration<PostalCode>
    {
        public void Configure(EntityTypeBuilder<PostalCode> builder)
        {
            builder.ToTable("PostalCode", "core");

            builder.Property(e => e.Bspname)
                .HasColumnName("BSPName")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Bspnumber)
                .HasColumnName("BSPNumber")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DeliveryOffice)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.PareclZone)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PostCode)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Street)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.PostalCodeCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostalCode_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.PostalCodeLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_PostalCode_Contact_UpdatedBy");

            builder.HasOne(d => d.State)
                .WithMany(p => p.PostalCodes)
                .HasForeignKey(d => d.StateId);

            builder.HasOne(d => d.Country)
                .WithMany(p => p.PostalCodes)
                .HasForeignKey(d => d.CountryId);
        }
    }
}