using Softcode.GTex.Data.Mappings.Views;
using Softcode.GTex.Data.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace Softcode.GTex.Data
{
    public class ApplicationViewDbContext : DbContext
    {
        public ApplicationViewDbContext(DbContextOptions<ApplicationViewDbContext> options)
            : base(options)
        {
        }

        public virtual DbQuery<CompanyView> CompanyView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new CompanyViewMapping());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}
