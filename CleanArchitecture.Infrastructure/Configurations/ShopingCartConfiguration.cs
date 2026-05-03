using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class ShopingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(e => e.CartID);
            builder.HasOne(e => e.User)
                .WithOne(e => e.ShoppingCart)
                .HasForeignKey<ShoppingCart>(e => e.UserID);
        }
    }
}
