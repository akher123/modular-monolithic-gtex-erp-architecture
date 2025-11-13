using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EmployeeSiteMapping : IEntityTypeConfiguration<EmployeeSite>
    {
        public void Configure(EntityTypeBuilder<EmployeeSite> builder)
        {
            builder.ToTable("EmployeeSite", "hrm");

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeSites)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.BusinessProfileSite)
              .WithMany(p => p.EmployeeSites)
              .HasForeignKey(d => d.SiteId)
              .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
