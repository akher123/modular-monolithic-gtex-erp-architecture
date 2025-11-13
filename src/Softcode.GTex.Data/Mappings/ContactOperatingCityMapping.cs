using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class ContactOperatingCityMapping : IEntityTypeConfiguration<ContactOperatingCity>
    {
        public void Configure(EntityTypeBuilder<ContactOperatingCity> builder)
        {
            builder.ToTable("ContactOperatingCity", "core");

            builder.HasIndex(e => new { e.ContactId, e.OperatingCity })
                .HasName("UN_ContactOperatingCity")
                .IsUnique();

            builder.Property(e => e.OperatingCity)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.Contact)
                .WithMany(p => p.ContactOperatingCities)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}