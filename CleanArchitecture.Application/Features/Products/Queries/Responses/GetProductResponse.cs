namespace CleanArchitecture.Application.Features.Products.Queries.Responses
{
    public class GetProductResponse
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public List<string> Sizes { get; set; }
        public int TotalUnitsSold { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string BrandName { get; set; }
        public int BrandID { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
