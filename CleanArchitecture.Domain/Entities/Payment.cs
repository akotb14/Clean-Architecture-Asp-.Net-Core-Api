namespace CleanArchitecture.Domain.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "tbc";
        public string PaymentStatus { get; set; } = "pending";
        public Order Order { get; set; }
    }
}