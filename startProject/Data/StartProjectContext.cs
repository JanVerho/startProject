using Microsoft.EntityFrameworkCore;
using startProject.Data.Configurations;
using startProject.Model;

namespace startProject.Data
{
    public class StartProjectContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source=startProject.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfigurations()).Seed();
        }
    }
}