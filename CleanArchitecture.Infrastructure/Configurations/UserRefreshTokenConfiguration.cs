using CleanArchitecture.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.user).WithMany(e => e.UserRefreshTokens).HasForeignKey(e => e.UserId);
            builder.Property(e => e.IsUsed).HasDefaultValue(true);
            builder.Property(e => e.IsRevoked).HasDefaultValue(false);

        }
    }
}
