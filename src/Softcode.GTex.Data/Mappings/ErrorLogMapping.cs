
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class ErrorLogMapping : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.ToTable("ErrorLog", "core");

            builder.Property(e => e.ApplicationVersion)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.DomainName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ErrorDateTime).HasColumnType("datetime");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.MachineUserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Message).IsUnicode(false);

            builder.Property(e => e.Osversion)
                .HasColumnName("OSVersion")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Resolution).IsUnicode(false);

            builder.Property(e => e.Source).IsUnicode(false);

            builder.Property(e => e.StackTrace).IsUnicode(false);

            builder.Property(e => e.TargetSite).IsUnicode(false);

            builder.Property(e => e.UserIp)
                .HasColumnName("UserIP")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.UserLocation)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.UserMachineName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.ErrorLogs)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ErrorLog_BusinessProfile");
        }
    }
}
