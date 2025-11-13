
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EntityLayoutTemplateMapping : IEntityTypeConfiguration<EntityLayoutTemplate>
    {
        public void Configure(EntityTypeBuilder<EntityLayoutTemplate> builder)
        {
            builder.ToTable("EntityLayoutTemplate", "core");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Description)
                .HasMaxLength(800)
                .IsUnicode(false);

            builder.Property(e => e.Guid)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.TimeStamp).IsRowVersion();

            builder.HasOne(d => d.BusinessProfile)
                .WithMany(p => p.EntityLayoutTemplates)
                .HasForeignKey(d => d.BusinessProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityLayoutTemplate_BusinessProfile");

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.EntityLayoutTemplateCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityLayoutTemplate_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.EntityLayoutTemplateLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_EntityLayoutTemplate_Contact_UpdatedBy");
        }
    }
}