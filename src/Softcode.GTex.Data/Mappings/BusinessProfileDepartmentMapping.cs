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
    public class BusinessProfileDepartmentMapping : IEntityTypeConfiguration<BusinessProfileDepartment>
    {
        public void Configure(EntityTypeBuilder<BusinessProfileDepartment> builder)
        {
            builder.HasOne(d => d.BusinessProfile)
           .WithMany(p => p.BusinessProfileDepartments)
           .HasForeignKey(d => d.BusinessProfileId);

        }
    }
}
