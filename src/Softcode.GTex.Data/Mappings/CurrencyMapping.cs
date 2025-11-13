
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class CurrencyMapping : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currency", "core");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 4)");

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Isocode)
                .IsRequired()
                .HasColumnName("ISOCode")
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.Precision).HasDefaultValueSql("((2))");

            builder.Property(e => e.Symbol).HasMaxLength(10);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.CurrencyCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Currency_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.CurrencyLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_Currency_Contact_UpdatedBy");
        }
    }
}