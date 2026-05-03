using CleanArchitecture.Domain.Entities.Identity;

namespace CleanArchitecture.Domain.Entities
{
    public class Wishlist
    {
        public int WishlistID { get; set; }
        public string UserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public User User { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
