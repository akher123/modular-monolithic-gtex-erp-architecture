using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class CustomCategoryMapTypeMapping : IEntityTypeConfiguration<CustomCategoryMapType>
    {
        public void Configure(EntityTypeBuilder<CustomCategoryMapType> builder)
        {
            builder.ToTable("CustomCategoryMapType", "core");

            builder.HasIndex(e => e.Name)
                .HasName("UK_CustomCategoryMapType")
                .IsUnique();

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
