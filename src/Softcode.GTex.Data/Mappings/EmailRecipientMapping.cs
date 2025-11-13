using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EmailRecipientMapping : IEntityTypeConfiguration<EmailRecipient>
    {
        public void Configure(EntityTypeBuilder<EmailRecipient> builder)
        {
            builder.ToTable("EmailRecipient", "service");

            builder.HasDiscriminator<string>("RecipientType")
           .HasValue<EmailRecipientTo>("To")
           .HasValue<EmailRecipientCc>("Cc")
           .HasValue<EmailRecipientBcc>("Bcc");

            builder.Property(e => e.Email)
                          .IsRequired()
                          .HasMaxLength(100)
                          .IsUnicode(false);

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.EmailJobQueue)
                .WithMany(p => p.EmailRecipients)
                .HasForeignKey(d => d.EmailJobQueueId);
        }
    }
}
