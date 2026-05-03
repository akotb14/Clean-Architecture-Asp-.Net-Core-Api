using CleanArchitecture.Domain.Entities.Identity;

namespace CleanArchitecture.Domain.Entities
{
    public class ShoppingCart
    {
        public int CartID { get; set; }
        public string UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
