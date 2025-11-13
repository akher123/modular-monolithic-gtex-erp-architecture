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
    public class BusinessCategoryMapping : IEntityTypeConfiguration<BusinessCategory>
    {
        public void Configure(EntityTypeBuilder<BusinessCategory> builder)
        {
            builder.ToTable("BusinessCategory", "core");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.ActionKey)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.BusinessCategoryType)
                .WithMany(p => p.BusinessCategories)
                .HasForeignKey(d => d.BusinessCategoryTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
