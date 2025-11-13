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
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee", "hrm");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.DeskId)
                .HasColumnName("DeskID")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.EmployeeId)
                .IsRequired()
                .HasColumnName("EmployeeID")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ExternalPartnerId)
                .HasColumnName("ExternalPartnerID")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Floor)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.JobCeasedDate).HasColumnType("date");

            builder.Property(e => e.JobCeasedReason)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.JobCommenceDate).HasColumnType("date");

            builder.Property(e => e.JobDescription)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.ProbationEndingDate).HasColumnType("date");

            builder.HasOne(d => d.BusinessUnit)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.BusinessUnitId);

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId);

            builder.HasOne(d => d.EmploymentType)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmploymentTypeId);

            builder.HasOne(d => d.Contact)
                .WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Contact_ContactId");

            builder.HasOne(d => d.Region)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.RegionId);

            builder.HasOne(d => d.Supervisor)
                .WithMany(p => p.InverseSupervisors)
                .HasForeignKey(d => d.SupervisorId);
        }
    }
}
