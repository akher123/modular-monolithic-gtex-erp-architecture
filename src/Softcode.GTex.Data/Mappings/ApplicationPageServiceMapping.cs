using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class ApplicationPageServiceMapping : IEntityTypeConfiguration<ApplicationPageService>
    {
        public void Configure(EntityTypeBuilder<ApplicationPageService> builder)
        {
            builder.ToTable("ApplicationPageService", "core");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.ServiceName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ServiceType)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.ServiceUrl)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.ApplicationPageServiceCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .HasConstraintName("FK_ApplicationPageService_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.ApplicationPageServiceLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_ApplicationPageService_Contact_UpdatedBy");

            builder.HasOne(d => d.Page)
                .WithMany(p => p.ApplicationPageServices)
                .HasForeignKey(d => d.PageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationPageService_ApplicationPage");
        }
    }
}
