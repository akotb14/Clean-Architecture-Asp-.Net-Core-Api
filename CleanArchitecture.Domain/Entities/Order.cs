using CleanArchitecture.Domain.Entities.Identity;

namespace CleanArchitecture.Domain.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int OrderIdPaymob { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Payment Payment { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public ICollection<Discount> Discounts { get; set; }

    }
}
