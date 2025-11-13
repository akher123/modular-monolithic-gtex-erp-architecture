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
    public class ApplicationPageListFieldMapping : IEntityTypeConfiguration<ApplicationPageListField>
    {
        public void Configure(EntityTypeBuilder<ApplicationPageListField> builder)
        {
            builder.ToTable("ApplicationPageListField", "core");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Alignment)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("((Left))");

            builder.Property(e => e.Caption)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CellTemplate)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ColumnFilterEnabled)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DataType)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ReadOnly)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.RowFilterEnabled)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.SortEnabled)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Visible)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.CreatedByContact)
                .WithMany(p => p.ApplicationPageFieldDetailCreatedByContacts)
                .HasForeignKey(d => d.CreatedByContactId)
                .HasConstraintName("FK_ApplicationPageFieldDetail_Contact_CreatedBy");

            builder.HasOne(d => d.LastUpdatedByContact)
                .WithMany(p => p.ApplicationPageFieldDetailLastUpdatedByContacts)
                .HasForeignKey(d => d.LastUpdatedByContactId)
                .HasConstraintName("FK_ApplicationPageFieldDetail_Contact_UpdatedBy");

            builder.HasOne(d => d.Page)
                .WithMany(p => p.ApplicationPageListFields)
                .HasForeignKey(d => d.PageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationPageFieldDetail_ApplicationPage");
        }
    }
}
