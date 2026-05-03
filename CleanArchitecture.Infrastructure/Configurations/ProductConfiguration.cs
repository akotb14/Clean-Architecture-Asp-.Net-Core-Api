using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ProductID);
            builder.Property(e => e.Name).HasMaxLength(200);
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.HasOne(e => e.Brand)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.BrandID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Category)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
