using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Commands.Requests
{
    public record EditProductCommad : IRequest<Response<string>>
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public List<string> Sizes { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        public string ImageURL { get; set; }

        public int TotalUnitsSold { get; set; }   // Track total sales
        public DateTime LastSoldDate { get; set; }

        public DateTime? ModifiedDate => DateTime.UtcNow;


    }
}
