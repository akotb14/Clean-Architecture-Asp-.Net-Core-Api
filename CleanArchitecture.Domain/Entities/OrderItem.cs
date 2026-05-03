namespace CleanArchitecture.Domain.Entities
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Size { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}