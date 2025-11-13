using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    class SystemEntityRightDependencyMapping : IEntityTypeConfiguration<SystemEntityRightDependency>
    {
        public void Configure(EntityTypeBuilder<SystemEntityRightDependency> builder)
        {
            builder.ToTable("SystemEntityRightDependency", "security");

            builder.HasOne(d => d.DependentRight)
                .WithMany(p => p.SystemEntityRightDependencyDependentRights)
                .HasForeignKey(d => d.DependentRightId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Right)
                .WithMany(p => p.SystemEntityRightDependencyRights)
                .HasForeignKey(d => d.RightId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
