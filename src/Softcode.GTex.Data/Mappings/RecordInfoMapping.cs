using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.Data.Mappings
{
    public class RecordInfoMapping : IEntityTypeConfiguration<RecordInfo>
    {        
        public void Configure(EntityTypeBuilder<RecordInfo> builder)
        {
            builder.ToTable("RecordInfo", "core");
            builder.HasIndex(e => e.Id)
                    .HasName("PK_RecordInfo")
                    .IsUnique();
            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            builder.HasOne(d => d.EntityType)
              .WithMany(p => p.RecordInfos)
              .HasForeignKey(d => d.EntityTypeId)
              .HasConstraintName("FK_RecordInfo_EntityType_EntityTypeId");
        }
    }
}
