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
    public class ApplicationRoleRightMapping : IEntityTypeConfiguration<ApplicationRoleRight>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleRight> builder)
        {
            builder.ToTable("ApplicationRoleRight", "security");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.RoleId)
                .IsRequired()
                .HasMaxLength(450);

            builder.HasOne(d => d.SystemEntityRight)
                .WithMany(p => p.ApplicationRoleRights)
                .HasForeignKey(d => d.RightId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Role)
                .WithMany(p => p.RoleRights)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_ApplicationRoleRight_ApplicationRoleRight_RoleId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
