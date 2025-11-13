using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class CountryMapping : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country", "core");

            builder.Property(e => e.CountryCode)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.CountryName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.CountryCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Country_Contact_CreatedBy");

            builder.HasOne(d => d.Currency)
                .WithMany(p => p.Countries)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("FK_Country_Currency");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.CountryLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_Country_Contact_UpdatedBy");
        }
    }
}