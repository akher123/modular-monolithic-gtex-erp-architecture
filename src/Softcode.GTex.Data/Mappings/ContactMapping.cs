
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{

    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact", "core");

            builder.Property(e => e.BusinessPhone)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.BusinessPhoneExt)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DateOfBirth)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Description).IsUnicode(false);

            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Email2)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Email3)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Fax)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.HomePhone)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.ImLoginId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ImLoginId2)
               .HasMaxLength(50)
               .IsUnicode(false);

            builder.Property(e => e.ImLoginId3)
               .HasMaxLength(50)
               .IsUnicode(false);

            builder.Property(e => e.LastExportedDateTime).HasColumnType("datetime");

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.PreferredName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.Property(e => e.TimeZoneId).HasMaxLength(200);

            builder.Property(e => e.Website)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.InverseCreatedByContact)
                .HasForeignKey(d => d.CreatedByContactId)
                .HasConstraintName("FK_Contact_Contact_CreatedBy");

            builder.HasOne(d => d.IdentityLicense)
                .WithMany(p => p.ContactIdentityLicenses)
                .HasForeignKey(d => d.IdentityLicenseId);

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.InverseLastUpdatedByContact)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_Contact_Contact_UpdatedBy");

            builder.HasOne(d => d.Photo)
                .WithMany(p => p.Contacts)
                .HasForeignKey(d => d.PhotoId);

            builder.HasOne(d => d.Postion)
                .WithMany(p => p.ContactPostions)
                .HasForeignKey(d => d.PostionId);

            builder.HasOne(d => d.Gender)
                .WithMany(p => p.ContactGenders)
                .HasForeignKey(d => d.GenderId);

            builder.HasOne(d => d.ImType)
                .WithMany(p => p.ContactImTypes)
                .HasForeignKey(d => d.ImTypeId);

            builder.HasOne(d => d.ImType2)
                .WithMany(p => p.ContactImTypes2)
                .HasForeignKey(d => d.ImTypeId2);

            builder.HasOne(d => d.ImType3)
                .WithMany(p => p.ContactImTypes3)
                .HasForeignKey(d => d.ImTypeId3);

            builder.HasOne(d => d.PreferredContactMethod)
                .WithMany(p => p.ContactPreferredContactMethods)
                .HasForeignKey(d => d.PreferredContactMethodId);

            builder.HasOne(d => d.TimeZone)
                .WithMany(p => p.Contacts)
                .HasForeignKey(d => d.TimeZoneId)
                .HasConstraintName("FK_Contact_TimeZone_TimezoneId");

            builder.HasOne(d => d.Title)
                .WithMany(p => p.ContactTitles)
                .HasForeignKey(d => d.TitleId)
                .HasConstraintName("FK_Contact_CustomCategory_TitleId");

            builder.HasOne(d => d.RecordInfo)
               .WithMany(p => p.Contacts)
               .HasForeignKey(d => d.UniqueEntityId)
               .HasConstraintName("FK_Contact_RecordInfo_RecordInfoId");
        }
    }
}