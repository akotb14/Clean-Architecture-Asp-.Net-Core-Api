using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(e => e.PaymentID);
            builder.HasOne(e => e.Order).
                WithOne(e => e.Payment)
                .HasForeignKey<Payment>(e => e.OrderID).
                OnDelete(DeleteBehavior.Restrict);
        }
    }
}
