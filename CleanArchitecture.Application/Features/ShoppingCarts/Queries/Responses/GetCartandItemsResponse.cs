namespace CleanArchitecture.Application.Features.ShoppingCarts.Queries.Responses
{
    public class GetCartandItemsResponse
    {
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ImageURL { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
