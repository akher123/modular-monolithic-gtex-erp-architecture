using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data.Mappings
{
    public class BusinessProfileSiteMapping : IEntityTypeConfiguration<BusinessProfileSite>
    {
        public void Configure(EntityTypeBuilder<BusinessProfileSite> builder)
        {
            builder.ToTable("BusinessProfileSite", "core");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Fax)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.SiteName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.HasOne(d => d.Address)
                .WithMany(p => p.BusinessProfileSites)
                .HasForeignKey(d => d.AddressId);

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.BusinessProfileSites)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.BusinessProfileSiteCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessProfileSite_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.BusinessProfileSiteLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_BusinessProfileSite_Contact_UpdatedBy");

            builder.HasOne(d => d.PrimaryContact)
                .WithMany(p => p.BusinessProfileSitePrimaryContacts)
                .HasForeignKey(d => d.PrimaryContactId);

            builder.HasOne(d => d.SiteMapDocument)
                .WithMany(p => p.BusinessProfileSites)
                .HasForeignKey(d => d.SiteMapDocumentId)
                .HasConstraintName("FK_BusinessProfileSite_DocumentMetadata_DocumentMetadataId");
        }
    }
}
