using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EmailJobQueueMapping : IEntityTypeConfiguration<EmailJobQueue>
    {
        public void Configure(EntityTypeBuilder<EmailJobQueue> builder)
        {
            builder.ToTable("EmailJobQueue", "service");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ExecutionTime).HasColumnType("datetime");

            builder.Property(e => e.SenderEmail)
                 .HasMaxLength(100)
                 .IsUnicode(false);

            builder.Property(e => e.SenderName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Subject)
           .IsRequired()
           .HasMaxLength(400)
           .IsUnicode(false);

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.LastExecutedOn).HasColumnType("datetime");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.NoOfAttempt).HasDefaultValueSql("((3))");

            builder.Property(e => e.ScheduleNote)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.ErrorText)
              .HasMaxLength(800)
              .IsUnicode(false);

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.EmailJobQueues)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.EmailJobQueueCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailJobQueue_Contact_CreatedBy");

            builder.HasOne(d => d.EmailTemplate)
                .WithMany(p => p.EmailJobQueues)
                .HasForeignKey(d => d.EmailTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.EmailType)
                .WithMany(p => p.EmailJobQueueEmailTypes)
                .HasForeignKey(d => d.EmailTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.EmailJobQueueLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_EmailJobQueue_Contact_UpdatedBy");

            
            builder.HasOne(d => d.Status)
               .WithMany(p => p.EmailJobQueues)
               .HasForeignKey(d => d.StatusId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
