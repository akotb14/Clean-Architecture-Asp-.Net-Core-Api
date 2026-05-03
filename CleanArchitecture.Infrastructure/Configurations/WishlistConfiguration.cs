using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(e => e.WishlistID);
            builder.HasOne(e => e.User).
                     WithOne(e => e.Wishlist)
                     .HasForeignKey<Wishlist>(e => e.UserID).
                     OnDelete(DeleteBehavior.Cascade);
        }
    }
}
