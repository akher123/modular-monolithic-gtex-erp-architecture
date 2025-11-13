using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class CommunicationFileStoreMapping : IEntityTypeConfiguration<CommunicationFileStore>
    {
        public void Configure(EntityTypeBuilder<CommunicationFileStore> builder)
        {
            builder.ToTable("CommunicationFileStore", "service");

            builder.HasOne(d => d.Communication)
                .WithMany(p => p.CommunicationFileStores)
                .HasForeignKey(d => d.CommunicationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.DocumentFileStore)
                .WithMany(p => p.CommunicationFileStores)
                .HasForeignKey(d => d.DocumentFileStoreId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
