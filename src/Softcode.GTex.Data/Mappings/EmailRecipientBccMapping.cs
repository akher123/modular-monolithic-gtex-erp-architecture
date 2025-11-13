using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EmailRecipientBccMapping : IEntityTypeConfiguration<EmailRecipientBcc>
    {
        public void Configure(EntityTypeBuilder<EmailRecipientBcc> builder)
        {
            //builder.ToTable("EmailRecipientBcc", "service");

            //builder.Property(e => e.Id).ValueGeneratedNever();

            //builder.Property(e => e.Email)
            //    .IsRequired()
            //    .HasMaxLength(100)
            //    .IsUnicode(false);

            //builder.Property(e => e.Name)
            //    .HasMaxLength(100)
            //    .IsUnicode(false);
        }
    }
}
