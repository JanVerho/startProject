using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using startProject.Model;

namespace startProject.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        // IEntityTypeConfiguration moet method Configure hebben anders error
        public void Configure(EntityTypeBuilder<Product> builder)
        {
        }
    }
}