namespace CleanArchitecture.Domain.Helper.Dtos
{
    public class OrderItemsDto
    {
        public string name { get; set; }
        public decimal amount_cents { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
    }
}
