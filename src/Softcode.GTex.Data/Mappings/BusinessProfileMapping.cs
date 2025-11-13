using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{

    public class BusinessProfileMapping : IEntityTypeConfiguration<BusinessProfile>
    {
        public void Configure(EntityTypeBuilder<BusinessProfile> builder)
        {
            builder.ToTable("BusinessProfile", "core");

            builder.Property(e => e.CompId)
                .HasColumnName("CompId")
                .HasMaxLength(3)
                .IsUnicode(true);

            builder.Property(e => e.DomainName)
                .HasColumnName("DomainName")
                .HasMaxLength(20)
                .IsUnicode(true);

            builder.Property(e => e.Abn)
                .HasColumnName("ABN")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Acn)
                .HasColumnName("ACN")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.ApplicationUrl)
                .HasColumnName("ApplicationURL")
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

           
            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Fax)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Number)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.SecondaryEmail)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SecondaryPhone)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.Property(e => e.TimeZoneId)
                .HasColumnName("TimeZoneID")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.UseSameConfigForPayrollIntegration)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Website)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.BusinessProfileCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessProfile_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.BusinessProfileLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_BusinessProfile_Contact_UpdatedBy");

            builder.HasOne(d => d.Logo)
                .WithMany(p => p.BusinessProfiles)
                .HasForeignKey(d => d.LogoId);

            builder.HasOne(d => d.RecordInfo)
             .WithMany(p => p.BusinessProfiles)
             .HasForeignKey(d => d.UniqueEntityId)             
             .HasConstraintName("FK_BusinessProfile_RecordInfo_RecordInfoId");
        }
    }
}