using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data.Mappings
{
    public class BusinessCategoryMapTypeMapping : IEntityTypeConfiguration<BusinessCategoryMapType>
    {
        public void Configure(EntityTypeBuilder<BusinessCategoryMapType> builder)
        {
            builder.ToTable("BusinessCategoryMapType", "core");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Value)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.BusinessCategory)
                .WithMany(p => p.BusinessCategoryMapTypes)
                .HasForeignKey(d => d.BusinessCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
