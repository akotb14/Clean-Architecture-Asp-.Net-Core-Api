using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class ShippingMethodConfiguration : IEntityTypeConfiguration<ShippingMethod>
    {
        public void Configure(EntityTypeBuilder<ShippingMethod> builder)
        {
            builder.HasKey(e => e.ShippingMethodID);
            builder.HasOne(e => e.Order).WithOne(e => e.ShippingMethod).HasForeignKey<ShippingMethod>(e => e.OrderID);
        }
    }
}
