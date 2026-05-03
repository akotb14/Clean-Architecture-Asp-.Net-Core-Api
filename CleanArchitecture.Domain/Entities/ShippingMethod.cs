namespace CleanArchitecture.Domain.Entities
{
    public class ShippingMethod
    {
        public int ShippingMethodID { get; set; }
        public decimal Cost { get; set; } = 15;
        public int EstimatedDeliveryDays { get; set; } = 5;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }
    }


}
