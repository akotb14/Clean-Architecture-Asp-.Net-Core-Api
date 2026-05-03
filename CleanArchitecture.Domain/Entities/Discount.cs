using CleanArchitecture.Domain.Entities.Identity;

namespace CleanArchitecture.Domain.Entities
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public string Name { get; set; } // e.g., "Summer Sale 20% Off"
        public string Code { get; set; } // e.g., "SUMMER20"
        public decimal? Percentage { get; set; } // e.g., 20% off
        public decimal? Amount { get; set; } // e.g., $10 off
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? ProductID { get; set; } // Apply to a specific product
        public Product Product { get; set; }
        public int? CategoryID { get; set; } // Apply to a specific category
        public Category Category { get; set; }
        public string? UserID { get; set; } // Apply to a specific user
        public User User { get; set; }
        public int? OrderID { get; set; } // Apply to a specific order
        public Order Order { get; set; }
        public int? UsageLimit { get; set; } // Limit the number of times the discount can be used

    }
}
