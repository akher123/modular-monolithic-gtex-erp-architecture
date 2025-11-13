using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class TimeZoneMapping : IEntityTypeConfiguration<TimeZone>
    {
        public void Configure(EntityTypeBuilder<TimeZone> builder)
        {
            builder.ToTable("TimeZone", "core");

            builder.Property(e => e.Id)
                .HasMaxLength(200)
                .ValueGeneratedNever();

            builder.Property(e => e.DaylightDisplayName).HasMaxLength(200);

            builder.Property(e => e.DaylightSavingEndDateTime).HasColumnType("datetime");

            builder.Property(e => e.DaylightSavingStartDateTime).HasColumnType("datetime");

            builder.Property(e => e.DisplayName).HasMaxLength(200);
        }
    }
}
