using AutoMapper;
using CleanArchitecture.Application.Features.Products.Commands.Requests;
using CleanArchitecture.Application.Features.Products.Queries.Responses;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mapping
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<AddProductCommad, Product>();
            CreateMap<EditProductCommad, Product>();
            CreateMap<Product, GetProductResponse>()
                .ForMember(d => d.CategoryName, s => s.MapFrom(op => op.Category.CategoryName))
                .ForMember(d => d.BrandName, s => s.MapFrom(op => op.Brand.BrandName));

        }
    }
}
