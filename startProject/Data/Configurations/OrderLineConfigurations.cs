using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using startProject.Model;

namespace startProject.Data.Configurations
{
    public class OrderLineConfigurations : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
        }
    }
}