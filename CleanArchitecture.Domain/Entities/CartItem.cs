namespace CleanArchitecture.Domain.Entities
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public ShoppingCart ShoppingCart { get; set; }
        public Product Product { get; set; }
    }
}