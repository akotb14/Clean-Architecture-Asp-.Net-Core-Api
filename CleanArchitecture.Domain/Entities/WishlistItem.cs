namespace CleanArchitecture.Domain.Entities
{
    public class WishlistItem
    {
        public int WishlistItemID { get; set; }
        public int WishlistID { get; set; }
        public int ProductID { get; set; }
        public DateTime AddedDate { get; set; }

        public Wishlist Wishlist { get; set; }
        public Product Product { get; set; }
    }

}