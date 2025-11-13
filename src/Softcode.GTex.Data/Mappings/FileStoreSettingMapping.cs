
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Softcode.GTex.Data.Mappings
{
    public class FileStoreSettingMapping : IEntityTypeConfiguration<FileStoreSetting>
    {
        public void Configure(EntityTypeBuilder<FileStoreSetting> builder)
        {
            builder.ToTable("FileStoreSetting", "core");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.HostDomain)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.HostIp)
                .IsRequired()
                .HasColumnName("HostIP")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.HostName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.HostPassword).IsUnicode(false);

            builder.Property(e => e.HostUsername)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.ShareDirectoryPath)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.ShareName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.FileStoreSettings)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FileStoreSetting_BusinessProfile");

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.FileStoreSettingCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.FileStoreSettingLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId);
        }
    }
}
