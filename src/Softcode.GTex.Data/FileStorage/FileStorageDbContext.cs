using Softcode.GTex.Data.FileStorage.Models;
using Microsoft.EntityFrameworkCore;

namespace Softcode.GTex.Data.FileStorage
{
    public class FileStorageDbContext : DbContext
    {
        public FileStorageDbContext(DbContextOptions<FileStorageDbContext> options)
          : base(options)
        {
        }

        public virtual DbSet<FileStore> FileStores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileStore>(entity =>
            {
                entity.ToTable("FileStore");

                entity.HasIndex(e => e.Id)
                    .HasName("UK_FileStore_Id")
                    .IsUnique();
                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                //entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FileContent).IsRequired();

                entity.Property(e => e.TimeStamp).IsRowVersion();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("data source=itm-sql-01;initial catalog=Fw_v6.0_dev_storage;Trusted_Connection=True;");
            }
        }
    }
}
