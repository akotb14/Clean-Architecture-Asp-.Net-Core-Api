using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.CategoryID);
            builder.Property(e => e.CategoryName).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(800);
            builder.Property(e => e.ParentCategoryID).IsRequired(false).HasDefaultValue(null);
            builder.HasOne(e => e.ParentCategory).WithMany(e => e.SubCategories)
                .HasForeignKey(e => e.ParentCategoryID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
