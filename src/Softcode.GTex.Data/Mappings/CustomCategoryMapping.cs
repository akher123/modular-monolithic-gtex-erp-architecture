using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class CustomCategoryMapping : IEntityTypeConfiguration<CustomCategory>
    {
        public void Configure(EntityTypeBuilder<CustomCategory> builder)
        {
            builder.ToTable("CustomCategory", "core");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Desciption)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.CustomCategories)
                .HasForeignKey(d => d.BusinessProfileId)
                .HasConstraintName("FK_CustomCategory_BusinessProfile");

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.CustomCategoryCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.CustomCategoryMapTypeOption)
                .WithMany(p => p.CustomCategories)
                .HasForeignKey(d => d.CustomCategoryMapTypeOptionId)
                .HasConstraintName("FK_CustomCategory_CustomCategoryMapTypeOption_MapTypeOptionId");

            builder.HasOne(d => d.CustomCategoryType)
                .WithMany(p => p.CustomCategories)
                .HasForeignKey(d => d.CustomCategoryTypeId)
                .HasConstraintName("FK_CustomCategory_CustomCategoryType_CustomCategoryTypeId")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.CustomCategoryLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId);
        }
    }
}