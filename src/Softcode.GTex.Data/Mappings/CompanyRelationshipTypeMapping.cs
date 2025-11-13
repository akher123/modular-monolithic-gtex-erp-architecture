using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class CompanyRelationshipTypeMapping : IEntityTypeConfiguration<CompanyRelationshipType>
    {
        public void Configure(EntityTypeBuilder<CompanyRelationshipType> builder)
        {
            builder.ToTable("CompanyRelationshipType", "crm");

            builder.HasOne(d => d.Company)
                .WithMany(p => p.CompanyRelationshipTypes)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyRelationshipType_Company");

            //builder.HasOne(d => d.RelationshipType)
            //    .WithMany(p => p.CompanyRelationshipTypes)
            //    .HasForeignKey(d => d.RelationshipTypeId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_CompanyRelationshipType_CustomCategory");
        }
    }
}
