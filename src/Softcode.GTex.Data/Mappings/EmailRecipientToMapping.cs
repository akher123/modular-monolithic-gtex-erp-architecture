using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EmailRecipientToMapping : IEntityTypeConfiguration<EmailRecipientTo>
    {
        public void Configure(EntityTypeBuilder<EmailRecipientTo> builder)
        {
            //builder.ToTable("EmailRecipientTo", "service");

            //builder.Property(e => e.Id).ValueGeneratedNever();

          
        }
    }
}
