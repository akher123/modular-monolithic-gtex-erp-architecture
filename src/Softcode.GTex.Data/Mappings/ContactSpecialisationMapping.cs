using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class ContactSpecialisationMapping : IEntityTypeConfiguration<ContactSpecialisation>
    {
        public void Configure(EntityTypeBuilder<ContactSpecialisation> builder)
        {
            builder.ToTable("ContactSpecialisation", "core");

            builder.Property(e => e.SpecialisationId)
                .IsRequired();

            builder.Property(e => e.ContactId)
                .IsRequired();

            builder.HasOne(d => d.Contact)                
                .WithMany(p => p.ContactSpecialisations)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.CustomCategory)
                .WithMany(p => p.ContactSpecialisations)
                .HasForeignKey(d => d.SpecialisationId);                
        }
    }
}
