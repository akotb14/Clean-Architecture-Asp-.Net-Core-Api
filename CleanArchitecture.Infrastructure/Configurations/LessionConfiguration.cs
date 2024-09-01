using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class LessionConfiguration : IEntityTypeConfiguration<Lession>
    {
        public void Configure(EntityTypeBuilder<Lession> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(800);


        }

    }
}
