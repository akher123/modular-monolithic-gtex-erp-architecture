using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class BusinessProfileAddressMapping : IEntityTypeConfiguration<BusinessProfileAddress>
    {
        public void Configure(EntityTypeBuilder<BusinessProfileAddress> builder)
        {
            
            //builder.ToTable("BusinessProfileAddress", "core");


            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.BusinessProfileAddresses)
                .HasForeignKey(d => d.BusinessProfileId);


        }

        //builder.Property(e => e.Id).ValueGeneratedNever();

        //builder.HasOne(d => d.Address)
        //    .WithOne(p => p.BusinessProfileAddress)
        //    .HasForeignKey<BusinessProfileAddress>(d => d.Id)
        //    .OnDelete(DeleteBehavior.ClientSetNull)
        //    .HasConstraintName("FK_BusinessProfileAddress_Addrress_BusinessProfileAddressId");
    }
}
