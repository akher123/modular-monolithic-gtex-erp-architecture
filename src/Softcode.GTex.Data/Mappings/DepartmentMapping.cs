using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Configuration;
using Softcode.GTex.Configuration;

namespace Softcode.GTex.Data.Mappings
{
    public class DepartmentMapping : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department", "core");


            //  builder.HasDiscriminator<int>("EntityTypeId")
            //.HasValue<BusinessProfileDepartment>(Configuration.ApplicationEntityType.BusinessProfile);

            builder.HasDiscriminator<int>("EntityTypeId")
            .HasValue<BusinessProfileDepartment>(ApplicationEntityType.BusinessProfile)
            .HasValue<CompanyDepartment>(ApplicationEntityType.Company);

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
             
            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.DepartmentCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Department_Contact_CreatedBy");
           
            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.DepartmentLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_Department_Contact_UpdatedBy");

            //builder.HasOne(d => d.EntityType)
            //    .WithMany(p => p.Department)
            //    .HasForeignKey(d => d.EntityTypeId)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(d => d.BusinessProfile)
            //    .WithMany(p => p.Department)
            //    .HasForeignKey(d => d.BusinessProfileId);

            //builder.HasOne(d => d.Company)
            //    .WithMany(p => p.Department)
            //    .HasForeignKey(d => d.CompanyId);

        }
    }
}
