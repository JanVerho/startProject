using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using startProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
