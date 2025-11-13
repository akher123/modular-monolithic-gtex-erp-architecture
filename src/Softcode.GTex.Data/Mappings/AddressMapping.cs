
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softcode.GTex.Data.Models;
using System.Configuration;

namespace Softcode.GTex.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address", "core");



            //builder.HasDiscriminator<int>("EntityTypeId")
            //   .HasValue<BusinessProfileAddress>(ApplicationEntityType.BusinessProfile);


            //builder.Property(e => e.EntityTypeId)
            //    .HasColumnName("EntityTypeId");

            builder.HasDiscriminator<int>("EntityTypeId")
           .HasValue<BusinessProfileAddress>(Configuration.ApplicationEntityType.BusinessProfile)
           .HasValue<ContactAddress>(Configuration.ApplicationEntityType.Contact);

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");



            builder.Property(e => e.PostCode)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.Street)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Suburb)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.HasOne(d => d.AddressType)
                .WithMany(p => p.Addresses)
                .HasForeignKey(d => d.AddressTypeId);

            builder.HasOne(d => d.Country)
                .WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull);



            builder.HasOne(d => d.State)
                .WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Address_State_StateId");
                

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.AddressCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.AddressLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_Address_Contact_UpdatedBy");

        }


    }
}
