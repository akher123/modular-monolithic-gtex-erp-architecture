
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EmailServerMapping : IEntityTypeConfiguration<EmailServer>
    {
        public void Configure(EntityTypeBuilder<EmailServer> builder)
        {
            builder.ToTable("EmailServer", "core");

            builder.Property(e => e.CopyToEmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);


            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.OutgoingServer)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Password).IsUnicode(false);

            builder.Property(e => e.ReplyToEmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SenderId)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SenderName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.Property(e => e.UseSslforOutgoing).HasColumnName("UseSSLForOutgoing");

            builder.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.ServerCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Server_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.ServerLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_Server_Contact_UpdatedBy");

            builder.HasOne(d => d.AuthenticationType)
                .WithMany(p => p.ServerAuthenticationTypes)
                .HasForeignKey(d => d.AuthenticationTypeId)
                .HasConstraintName("FK_Server_BusinessCategory_AuthenticationTypeId");

            builder.HasOne(d => d.Protocol)
                .WithMany(p => p.ServerProtocols)
                .HasForeignKey(d => d.ProtocolId)
                .HasConstraintName("FK_Server_BusinessCategory_ProtocolId");

            builder.HasOne(d => d.SenderOption)
                .WithMany(p => p.ServerSenderOptions)
                .HasForeignKey(d => d.SenderOptionId)
                .HasConstraintName("FK_Server_BusinessCategory_SenderOptionId");

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.Servers)
                .HasForeignKey(d => d.BusinessProfileId)
                .HasConstraintName("FK_Server_BusinessProfile_BusinessProfileId");
        }
    }
}
