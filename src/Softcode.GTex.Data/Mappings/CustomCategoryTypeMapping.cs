using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Softcode.GTex.Data.Mappings
{
    public class CustomCategoryTypeMapping : IEntityTypeConfiguration<CustomCategoryType>
    {
        public void Configure(EntityTypeBuilder<CustomCategoryType> builder)
        {
            builder.ToTable("CustomCategoryType", "core");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.HelpText).IsUnicode(false);

            builder.HasIndex(e => e.RoutingKey)
               .HasName("UK_CustomCategoryType_RoutingKey")
               .IsUnique();

            builder.Property(e => e.ImageSource)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.ModuleName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.CustomCategoryMapType)
                .WithMany(p => p.CustomCategoryTypes)
                .HasForeignKey(d => d.CustomCategoryMapTypeId)
                .HasConstraintName("FK_CustomCategoryType_MapType_CustomCategoryMapTypeId");

            builder.HasOne(d => d.Right)
                .WithMany(p => p.CustomCategoryTypes)
                .HasForeignKey(d => d.RightId);
        }
    }
}
