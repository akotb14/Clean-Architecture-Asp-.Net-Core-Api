namespace CleanArchitecture.Application.Features.Orders.Queries.Responses
{
    public class GetOrdersResponse
    {
        public int OrderID { get; set; }
        public int OrderItemID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ImageUrl { get; set; }
        public string Size { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
