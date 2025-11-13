using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class PublicHolidayMapping : IEntityTypeConfiguration<PublicHoliday>
    {
        public void Configure(EntityTypeBuilder<PublicHoliday> builder)
        {
            builder.ToTable("PublicHoliday", "core");

            builder.HasIndex(e => new { e.EventDate, e.CountryId, e.StateId })
                .HasName("UN_PublicHoliday")
                .IsUnique();

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EventDate).HasColumnType("date");

            builder.Property(e => e.EventName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.Timestamp).IsRowVersion();

            builder.HasOne(d => d.Country)
                .WithMany(p => p.PublicHolidays)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PublicHoliday_Country");

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.PublicHolidayCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PublicHoliday_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.PublicHolidayLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_PublicHoliday_Contact_UpdatedBy");

            builder.HasOne(d => d.State)
                .WithMany(p => p.PublicHolidays)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_PublicHoliday_State");
        }
    }
}
