using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EmployeeCostCentreMapping : IEntityTypeConfiguration<EmployeeCostCentre>
    {
        public void Configure(EntityTypeBuilder<EmployeeCostCentre> builder)
        {
            builder.ToTable("EmployeeCostCentre", "hrm");

            builder.HasOne(d => d.CostCentre)
                .WithMany(p => p.EmployeeCostCentres)
                .HasForeignKey(d => d.CostCentreId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeCostCentres)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
