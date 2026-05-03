using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.HasKey(e => e.ReviewID);
            builder.HasOne(e => e.Product).
                     WithMany(e => e.ProductReviews)
                     .HasForeignKey(e => e.ProductID).
                     OnDelete(DeleteBehavior.Cascade);
        }
    }
}
