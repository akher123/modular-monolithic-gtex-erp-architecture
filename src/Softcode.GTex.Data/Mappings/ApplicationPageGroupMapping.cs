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
    class ApplicationPageGroupMapping : IEntityTypeConfiguration<ApplicationPageGroup>
    {
        public void Configure(EntityTypeBuilder<ApplicationPageGroup> builder)
        {
            builder.ToTable("ApplicationPageGroup", "core");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Page)
                .WithMany(p => p.ApplicationPageGroups)
                .HasForeignKey(d => d.PageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationDetailPageGroup_ApplicationPage");
        }

    }
}
