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
    public class CompanyDepartmentMapping : IEntityTypeConfiguration<CompanyDepartment>
    {
        public void Configure(EntityTypeBuilder<CompanyDepartment> builder)
        {
            builder.HasOne(d => d.Company)
               .WithMany(p => p.CompanyDepartments)
               .HasForeignKey(d => d.CompanyId);
        }
    }
}
