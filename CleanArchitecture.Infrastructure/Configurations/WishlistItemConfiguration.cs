using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.HasKey(e => e.WishlistItemID);
            builder.HasOne(e => e.Product).
                     WithMany(e => e.WishlistItems)
                     .HasForeignKey(e => e.ProductID).
                     OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(e => e.Wishlist).
                     WithMany(e => e.WishlistItems)
                     .HasForeignKey(e => e.WishlistID).
                     OnDelete(DeleteBehavior.Cascade);
        }
    }
}
