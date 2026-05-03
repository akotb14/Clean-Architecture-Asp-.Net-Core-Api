using CleanArchitecture.Application.ResultHandler;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.Products.Commands.Requests
{
    public record AddProductCommad : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public List<string> Sizes { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        public IFormFile ImageURL { get; set; }

        public int TotalUnitsSold { get; set; }   // Track total sales
        public DateTime LastSoldDate { get; set; }

        public DateTime CreatedDate => DateTime.UtcNow;

    }
}
