
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{

    public class CompanyOperatingStateMapping : IEntityTypeConfiguration<CompanyOperatingState>
    {
        public void Configure(EntityTypeBuilder<CompanyOperatingState> builder)
        {
            builder.ToTable("CompanyOperatingState", "crm");

            builder.HasOne(d => d.Company)
                .WithMany(p => p.CompanyOperatingStates)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyOperatingCountry_Company");

            builder.HasOne(d => d.OperatingState)
                .WithMany(p => p.CompanyOperatingStates)
                .HasForeignKey(d => d.OperatingStateId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

}