using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(e => e.ImageID);
            builder.HasOne(e => e.Product).
                     WithMany(e => e.ProductImages)
                     .HasForeignKey(e => e.ProductID).
                     OnDelete(DeleteBehavior.Cascade);
        }
    }
}
