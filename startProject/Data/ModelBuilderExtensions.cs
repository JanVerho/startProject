using Microsoft.EntityFrameworkCore;
using startProject.Model;

namespace startProject.Data
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder Seed(this ModelBuilder modelBuilder)
        {
            Product[] initialProducts = {
                new Product(1, "krokus", 8, 15),
                new Product(2, "narcis", 10, 20),
                new Product(3, "hyacint", 15, 30),
                new Product(4, "tulp", 25, 40),
                new Product(5, "violet", 25, 45),
                new Product(6, "roos", 30, 42),
                new Product(7, "zonnebloem", 20, 32),
                new Product(8, "margriet", 18, 28),
                new Product(9, "goudsbloem", 20, 30),
                new Product(10, "clematis", 30, 35),
            };
            foreach (Product p in initialProducts)
            {
                modelBuilder.Entity<Product>().HasData(p);
            }
            return modelBuilder;
        }
    }
}