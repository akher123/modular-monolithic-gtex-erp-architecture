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
    public class EmailAttachmentMapping : IEntityTypeConfiguration<EmailAttachment>
    {
        public void Configure(EntityTypeBuilder<EmailAttachment> builder)
        {
            builder.ToTable("EmailAttachment", "service");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.FileName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.FilePath)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.DocumentMetadata)
                .WithMany(p => p.EmailAttachments)
                .HasForeignKey(d => d.DocumentMetadataId)
                .HasConstraintName("FK_EmailAttachment_DocumentMetadata");

            builder.HasOne(d => d.EmailJobQueue)
                .WithMany(p => p.EmailAttachments)
                .HasForeignKey(d => d.EmailJobQueueId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
