using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class AccessLogMapping : IEntityTypeConfiguration<AccessLog>
    {
        public void Configure(EntityTypeBuilder<AccessLog> builder)
        {
            builder.ToTable("AccessLog", "core");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EntityName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.LogDateTime).HasColumnType("datetime");

            builder.Property(e => e.LogDescription).IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.Property(e => e.Token).IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.AccessLogCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccessLog_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.AccessLogLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_AccessLog_Contact_UpdatedBy");

            builder.HasOne(d => d.User)
                .WithMany(p => p.AccessLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }

}
