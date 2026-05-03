using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => e.OrderItemID);
            builder.HasOne(e => e.Product).
                WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.ProductID).
                OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Order).
                WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.OrderID).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
