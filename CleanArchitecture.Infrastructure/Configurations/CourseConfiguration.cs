using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(800);
            builder.HasMany(e => e.Lessions).WithOne(e => e.Course).HasForeignKey(e => e.CourseId);
        }
    }
}
