using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class BusinessCategoryTypeMapping : IEntityTypeConfiguration<BusinessCategoryType>
    {
        public void Configure(EntityTypeBuilder<BusinessCategoryType> builder)
        {
            builder.ToTable("BusinessCategoryType", "core");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.HasIndex(e => e.Name)
                .HasName("UK_BusinessCategoryType_Name")
                .IsUnique();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

        }
    }
}
