using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class DocumentMetadataMapping : IEntityTypeConfiguration<DocumentMetadata>
    {
        public void Configure(EntityTypeBuilder<DocumentMetadata> builder)
        {
            builder.ToTable("DocumentMetadata", "dms");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");
            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.Property(e => e.Keywords)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.DocumentMetadatas)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.DocumentMetadataCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentMetadata_Contact_CreatedBy");

            builder.HasOne(d => d.DocumentType)
                .WithMany(p => p.DocumentMetadatas)
                .HasForeignKey(d => d.DocumentTypeId);

            builder.HasOne(d => d.EntityType)
                .WithMany(p => p.DocumentMetadatas)
                .HasForeignKey(d => d.EntityTypeId);

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.DocumentMetadataLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_DocumentMetadata_Contact_UpdatedBy");
        }
    }
}
