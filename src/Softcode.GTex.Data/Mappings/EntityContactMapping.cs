
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EntityContactMapping : IEntityTypeConfiguration<EntityContact>
    {
        public void Configure(EntityTypeBuilder<EntityContact> builder)
        {
            builder.ToTable("EntityContact", "core");

            builder.HasOne(d => d.Contact)
                .WithMany(p => p.EntityContacts)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.EntityType)
               .WithMany(p => p.EntityContacts)
               .HasForeignKey(d => d.EntityTypeId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Company)
               .WithMany(p => p.CompanyEntityContacts)
               .HasForeignKey(d => d.EntityId)
               .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}