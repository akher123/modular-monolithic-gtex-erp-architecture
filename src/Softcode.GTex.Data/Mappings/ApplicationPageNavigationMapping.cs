using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Softcode.GTex.Data.Mappings
{
    public class ApplicationPageNavigationMapping : IEntityTypeConfiguration<ApplicationPageNavigation>
    {
        public void Configure(EntityTypeBuilder<ApplicationPageNavigation> builder)
        {
            builder.ToTable("ApplicationPageNavigation", "core");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.LinkName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.NavigateUrl)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.ApplicationPageNavigationCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .HasConstraintName("FK_ApplicationPageNavigation_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.ApplicationPageNavigationLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_ApplicationPageNavigation_Contact_UpdatedBy");

            builder.HasOne(d => d.Page)
                .WithMany(p => p.ApplicationPageNavigations)
                .HasForeignKey(d => d.PageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationPageNavigation_ApplicationPage");
        }
    }
}
