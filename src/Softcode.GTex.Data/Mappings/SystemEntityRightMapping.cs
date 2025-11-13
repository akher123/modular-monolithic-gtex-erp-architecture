using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    class SystemEntityRightMapping : IEntityTypeConfiguration<SystemEntityRight>
    {
        public void Configure(EntityTypeBuilder<SystemEntityRight> builder)
        {
            builder.ToTable("SystemEntityRight", "security");

            builder.HasIndex(e => new { e.RightKey, e.EntityId })
                .HasName("UK_SystemEntityRight")
                .IsUnique();

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.RightKey)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.HasOne(d => d.SystemEntity)
                .WithMany(p => p.SystemEntityRights)
                .HasForeignKey(d => d.EntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

    }
}
