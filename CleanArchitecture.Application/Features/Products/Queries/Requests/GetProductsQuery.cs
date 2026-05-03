using CleanArchitecture.Application.Features.Products.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Queries.Requests
{
    public class GetProductsQuery : IRequest<PaginatedResult<GetProductResponse>>
    {
        public List<int>? CategoryID { get; set; }
        public List<int>? BrandID { get; set; }
        public List<string>? Sizes { get; set; }
        public string? Search { get; set; }
        public string? Order { get; set; }
        public string? OrderDirection { get; set; }
        public int PageNumber { get; set; }
        public bool? IsBestSellers { get; set; } = false;
        public bool? IsAvailable { get; set; } = false;
        public int PageSize { get; set; }
    }
}
