using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class CommunicationMapping : IEntityTypeConfiguration<Communication>
    {
        public void Configure(EntityTypeBuilder<Communication> builder)
        {
            builder.ToTable("Communication", "service");

            builder.Property(e => e.CommunicationDateTime).HasColumnType("datetime");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.FollowupDate).HasColumnType("datetime");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.Notes).IsUnicode(false);

            builder.Property(e => e.ReminderDateTime).HasColumnType("datetime");

            builder.Property(e => e.Subject)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.Communications)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.CommunicationCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Communication_Contact_CreatedBy");

            builder.HasOne(d => d.EntityType)
                .WithMany(p => p.Communications)
                .HasForeignKey(d => d.EntityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.FollowupByContact)
                .WithMany(p => p.CommunicationFollowupByContacts)
                .HasForeignKey(d => d.FollowupByContactId);

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.CommunicationLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_Communication_Contact_UpdatedBy");

            builder.HasOne(d => d.MethodType)
                .WithMany(p => p.CommunicationMethodTypes)
                .HasForeignKey(d => d.MethodTypeId);

            builder.HasOne(d => d.Status)
                .WithMany(p => p.CommunicationStatus)
                .HasForeignKey(d => d.StatusId);
        }
    }
}
