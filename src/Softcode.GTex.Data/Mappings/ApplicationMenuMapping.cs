using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Softcode.GTex.Data.Mappings
{
    public class ApplicationMenuMapping : IEntityTypeConfiguration<ApplicationMenu>
    {
        public void Configure(EntityTypeBuilder<ApplicationMenu> builder)
        {
            builder.ToTable("ApplicationMenu", "core");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Caption)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.HelpText)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NavigateUrl)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.ApplicationMenuCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationMenu_Contact_CreatedBy");

            builder.HasOne(d => d.Entity)
                .WithMany(p => p.ApplicationMenus)
                .HasForeignKey(d => d.EntityId);

            builder.HasOne(d => d.EntityRight)
                .WithMany(p => p.ApplicationMenus)
                .HasForeignKey(d => d.EntityRightId)
                .HasConstraintName("FK_ApplicationMenu_SystemEntityRight_RightId");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.ApplicationMenuLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_ApplicationMenu_Contact_UpdatedBy");

            builder.HasOne(d => d.Page)
                .WithMany(p => p.ApplicationMenus)
                .HasForeignKey(d => d.PageId)
                .HasConstraintName("FK_ApplicationMenu_ApplicationPage");

             
            builder.HasOne(d => d.Parent)
               .WithMany(p => p.InverseParent)
               .HasForeignKey(d => d.ParentId)
               .HasConstraintName("FK_ApplicationMenu_ApplicationMenu_Parent");
        }
    }
}
