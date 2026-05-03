using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.OrderID);
            builder.HasOne(e => e.User).
                 WithMany(e => e.Orders)
                 .HasForeignKey(e => e.UserID).
                 OnDelete(DeleteBehavior.Restrict);
        }
    }
}
