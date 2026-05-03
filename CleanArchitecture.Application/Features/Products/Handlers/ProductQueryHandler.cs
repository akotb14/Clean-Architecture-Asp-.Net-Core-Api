using AutoMapper;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.Features.Products.Queries.Requests;
using CleanArchitecture.Application.Features.Products.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Infrastructure.Repositories.ProductRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Products.Handlers
{
    public class ProductQueryHandler : ResponseHandler,
        IRequestHandler<GetProductsQuery, PaginatedResult<GetProductResponse>>,
        IRequestHandler<GetProductByIdQuery, Response<GetProductResponse>>
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _mapper;

        public ProductQueryHandler(IProductRepository ProductRepository, IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<GetProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _ProductRepository.GetTableNoTracking().AsQueryable();
            var src = request.Search;
            if (request.IsAvailable == true && request.IsAvailable != null)
            {
                products = products.Where(e => e.StockQuantity > 0);
            }
            if (request.IsBestSellers == true && request.IsBestSellers != null)
            {
                products = products.OrderByDescending(e => e.TotalUnitsSold).
                    ThenByDescending(e => e.LastSoldDate);
            }
            else
            {
                //search 
                if (src != null)
                {
                    products = products.Where(e => e.Name.Contains(src) || e.Price.ToString().Contains(src) || e.Description.Contains(src));
                }
                // filter category and brand and size
                if (request.CategoryID != null && request.CategoryID.Any())
                {
                    products = products.Where(e => request.CategoryID.Contains(e.CategoryID));
                }
                if (request.BrandID != null && request.BrandID.Any())
                {
                    products = products.Where(e => request.BrandID.Contains(e.BrandID));
                }
                if (request.Sizes != null && request.Sizes.Any())
                {
                    products = products.Where(e => e.Sizes.Any(s => request.Sizes.Contains(s)));
                }
                //order
                products = _ProductRepository.OrderProduct(products, request.Order, request.OrderDirection);
            }

            var productsMapping = await _mapper.ProjectTo<GetProductResponse>(products).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return productsMapping;
        }

        public async Task<Response<GetProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _ProductRepository.GetTableNoTracking().Include(e => e.Category).Include(e => e.Brand).FirstOrDefaultAsync(e => e.ProductID == request.ProductID);
            if (product == null) { return NotFound<GetProductResponse>("Product not found"); }
            var productMapping = _mapper.Map<GetProductResponse>(product);
            return Success(productMapping);
        }
    }
}
