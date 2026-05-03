using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(e => e.BrandID);
            builder.Property(e => e.BrandName).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(800);
        }

    }
}
