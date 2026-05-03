using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(e => e.LogID);
            builder.HasOne(e => e.User).
                     WithMany(e => e.AuditLog)
                     .HasForeignKey(e => e.UserID).
                     OnDelete(DeleteBehavior.SetNull);
        }
    }

}
