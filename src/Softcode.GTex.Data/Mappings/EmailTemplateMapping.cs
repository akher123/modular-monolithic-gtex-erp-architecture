using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Softcode.GTex.Data.Mappings
{
    public class EmailTemplateMapping : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> entity)
        {
            entity.ToTable("EmailTemplate", "service");

            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasMaxLength(400)
                .IsUnicode(false);

            entity.HasOne(d => d.BusinessMapType)
                .WithMany(p => p.EmailTemplates)
                .HasForeignKey(d => d.BusinessMapTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.EmailTemplateCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailTemplate_Contact_CreatedBy");

            entity.HasOne(d => d.EmailServer)
                .WithMany(p => p.EmailTemplates)
                .HasForeignKey(d => d.EmailServerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmailTemplate_EmailServer_EmailServerId");

            entity.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.EmailTemplateLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_EmailTemplate_Contact_UpdatedBy");
        }
    }
}
