using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Softcode.GTex.Data.Mappings
{
    public class DocumentFileStoreMapping : IEntityTypeConfiguration<DocumentFileStore>
    {
        public void Configure(EntityTypeBuilder<DocumentFileStore> builder)
        {
            builder.ToTable("DocumentFileStore", "dms");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.FilePath)
                .HasMaxLength(400)
                .IsUnicode(false);


            builder.Property(e => e.FileName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.FileSizeInKb).HasColumnName("FileSizeInKB");


            builder.Property(e => e.MimeType)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.OrginalFileName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.DocumentFileStoreCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentFileStore_Contact_CreatedBy");

            builder.HasOne(d => d.DocumentMetadata)
                .WithMany(p => p.DocumentFileStores)
                .HasForeignKey(d => d.DocumentMetadataId);

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.DocumentFileStoreLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_DocumentFileStore_Contact_UpdatedBy");
        }
    }
}
