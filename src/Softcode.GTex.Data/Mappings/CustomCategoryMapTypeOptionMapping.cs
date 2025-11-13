using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Softcode.GTex.Data.Mappings
{
    public class CustomCategoryMapTypeOptionMapping : IEntityTypeConfiguration<CustomCategoryMapTypeOption>
    {
        public void Configure(EntityTypeBuilder<CustomCategoryMapTypeOption> builder)
        {
            builder.ToTable("CustomCategoryMapTypeOption", "core");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.CustomCategoryMapType)
                .WithMany(p => p.CustomCategoryMapTypeOptions)
                .HasForeignKey(d => d.CustomCategoryMapTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
