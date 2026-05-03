using Microsoft.AspNetCore.Identity;
namespace CleanArchitecture.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public User()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? ImageProfile { get; set; }
        public string? Code { get; set; }
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public ICollection<AuditLog> AuditLog { get; set; }
        public Wishlist Wishlist { get; set; }
        public ICollection<Discount> Discounts { get; set; }

    }
}
