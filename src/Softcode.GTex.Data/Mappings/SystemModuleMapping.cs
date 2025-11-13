using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Softcode.GTex.Data.Mappings
{

    public class SystemModuleMapping : IEntityTypeConfiguration<SystemModule>
    {
        public void Configure(EntityTypeBuilder<SystemModule> builder)
        {
            builder.HasKey(e => e.ModuleId);

            builder.ToTable("SystemModule", "security");

            builder.Property(e => e.ModuleId).ValueGeneratedNever();

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.ModuleName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();
         
        }
    }

}