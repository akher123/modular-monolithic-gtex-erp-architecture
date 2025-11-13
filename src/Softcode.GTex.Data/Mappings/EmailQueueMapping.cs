using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EmailQueueMapping : IEntityTypeConfiguration<EmailQueue>
    {
        public void Configure(EntityTypeBuilder<EmailQueue> builder)
        {
            builder.ToTable("EmailQueue", "service");

            builder.Property(e => e.Id);

            builder.Property(e => e.Body).IsRequired();

  
            builder.Property(e => e.ErrorText)
                .HasMaxLength(800)
                .IsUnicode(false);

   
            builder.Property(e => e.SentDateTime).HasColumnType("datetime");

            builder.Property(e => e.Subject).HasMaxLength(400);
            builder.Property(e => e.RecipientEmail).HasMaxLength(100);
            builder.Property(e => e.RecipientName).HasMaxLength(100);

            builder.HasOne(d => d.EmailJobQueue)
                .WithMany(p => p.EmailQueues)
                .HasForeignKey(d => d.EmailJobQueueId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Status)
                .WithMany(p => p.EmailQueues)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
