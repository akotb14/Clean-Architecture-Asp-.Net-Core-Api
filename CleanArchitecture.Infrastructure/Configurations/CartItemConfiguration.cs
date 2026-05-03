using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(e => e.CartItemID);
            builder.HasOne(e => e.ShoppingCart)
                .WithMany(e => e.CartItems)
                .HasForeignKey(e => e.CartID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(e => e.Product)
                .WithMany(e => e.CartItems)
                .HasForeignKey(e => e.ProductID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
