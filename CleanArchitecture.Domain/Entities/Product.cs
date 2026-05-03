namespace CleanArchitecture.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public List<string> Sizes { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        public string ImageURL { get; set; }

        public int TotalUnitsSold { get; set; }
        public DateTime LastSoldDate { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
        public ICollection<Discount> Discounts { get; set; }











    }
}
