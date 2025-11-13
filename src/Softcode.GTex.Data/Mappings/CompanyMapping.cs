using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class CompanyMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company", "crm");

            builder.Property(e => e.Abn)
                .HasColumnName("ABN")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Acn)
                .HasColumnName("ACN")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.AnnualTurnover).HasColumnType("numeric(18, 2)");

            builder.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Description).IsUnicode(false);

            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ExternalPartnerId)
                .HasColumnName("ExternalPartnerID")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Fax)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.MainPhone)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.MobilePhone)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.Property(e => e.TimeZoneId)
                .HasColumnName("TimeZoneID")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.TradeAs)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Website)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.Companies)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_BusinessProfile");

            builder.HasOne(d => d.CompanyType)
                .WithMany(p => p.CompanyCompanyTypes)
                .HasForeignKey(d => d.CompanyTypeId)
                .HasConstraintName("FK_Company_CustomCategory_CompanyType");

            builder.HasOne(d => d.Country)
                .WithMany(p => p.Companies)
                .HasForeignKey(d => d.CountryId);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.CompanyCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_Contact_CreatedBy");

            builder.HasOne(d => d.IndustryType)
                .WithMany(p => p.CompanyIndustryTypes)
                .HasForeignKey(d => d.IndustryTypeId)
                .HasConstraintName("FK_Company_CustomCategory_IndustryType");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.CompanyLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_Company_Contact_UpdatedBy");

            builder.HasOne(d => d.Logo)
                .WithMany(p => p.Companies)
                .HasForeignKey(d => d.LogoId)
                .HasConstraintName("FK_Company_Photo");

            builder.HasOne(d => d.PreferredContactMethod)
                .WithMany(p => p.CompanyPreferredContactMethods)
                .HasForeignKey(d => d.PreferredContactMethodId)
                .HasConstraintName("FK_Company_CustomCategory_PreferredContactMethod");

            builder.HasOne(d => d.PrimaryContact)
                .WithMany(p => p.CompanyPrimaryContacts)
                .HasForeignKey(d => d.PrimaryContactId);

            builder.HasOne(d => d.RatingType)
                .WithMany(p => p.CompanyRatingTypes)
                .HasForeignKey(d => d.RatingTypeId)
                .HasConstraintName("FK_Company_CustomCategory_Rating");

            builder.HasOne(d => d.State)
                .WithMany(p => p.Companies)
                .HasForeignKey(d => d.StateId);

            builder.HasOne(d => d.RecordInfo)
            .WithMany(p => p.Companies)
            .HasForeignKey(d => d.UniqueEntityId)
            .HasConstraintName("FK_Company_RecordInfo_RecordInfoId");
        }
    }
}