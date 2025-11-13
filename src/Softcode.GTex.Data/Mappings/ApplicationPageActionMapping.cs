using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data.Mappings
{
    class ApplicationPageActionMapping : IEntityTypeConfiguration<ApplicationPageAction>
    {
        public void Configure(EntityTypeBuilder<ApplicationPageAction> builder )
        {
            builder.ToTable("ApplicationPageAction", "core");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.ActionName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.NavigateUrl)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.Caption)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.ApplicationPageActionCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationPageAction_Contact");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.ApplicationPageActionLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_ApplicationPageAction_Contact1");

            builder.HasOne(d => d.Page)
                .WithMany(p => p.ApplicationPageActions)
                .HasForeignKey(d => d.PageId)
                .HasConstraintName("FK_ApplicationPageAction_ApplicationPage");
        }
    }
}
