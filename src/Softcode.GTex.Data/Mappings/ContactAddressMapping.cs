using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class ContactAddressMapping : IEntityTypeConfiguration<ContactAddress>
    {
        public void Configure(EntityTypeBuilder<ContactAddress> builder)
        {
            
            //builder.ToTable("BusinessProfileAddress", "core");


            builder.HasOne(d => d.Contact)
                .WithMany(p => p.ContactAddresses)
                .HasForeignKey(d => d.ContactId);


        }

        //builder.Property(e => e.Id).ValueGeneratedNever();

        //builder.HasOne(d => d.Address)
        //    .WithOne(p => p.BusinessProfileAddress)
        //    .HasForeignKey<BusinessProfileAddress>(d => d.Id)
        //    .OnDelete(DeleteBehavior.ClientSetNull)
        //    .HasConstraintName("FK_BusinessProfileAddress_Addrress_BusinessProfileAddressId");
    }
}
