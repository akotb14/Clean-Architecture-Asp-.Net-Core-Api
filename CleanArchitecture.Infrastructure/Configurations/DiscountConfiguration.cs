using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(e => e.DiscountID);
            builder.HasOne(d => d.Product)
                .WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.SetNull); // If the product is deleted, the discount remains but without a product association.

            builder.HasOne(d => d.Category)
                  .WithMany(c => c.Discounts)
                  .HasForeignKey(d => d.CategoryID)
                  .OnDelete(DeleteBehavior.SetNull); // If the category is deleted, the discount remains but without a category association.

            builder.HasOne(d => d.Order)
                .WithMany(o => o.Discounts)
                .HasForeignKey(d => d.OrderID)
                .OnDelete(DeleteBehavior.SetNull); // If the order is deleted, the discount remains but without an order association.

            builder.HasOne(d => d.User)
                   .WithMany(u => u.Discounts)
                   .HasForeignKey(d => d.UserID)
                   .OnDelete(DeleteBehavior.SetNull); // If the user is deleted, the discount remains but without a user association.

        }
    }
}
