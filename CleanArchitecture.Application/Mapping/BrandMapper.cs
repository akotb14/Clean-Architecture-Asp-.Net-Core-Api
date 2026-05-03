using AutoMapper;
using CleanArchitecture.Application.Features.Brands.Commands.Requests;
using CleanArchitecture.Application.Features.Brands.Queries.Responses;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mapping
{
    public class BrandMapper : Profile
    {
        public BrandMapper()
        {
            CreateMap<AddBrandCommad, Brand>();
            CreateMap<EditBrandCommad, Brand>();
            CreateMap<Brand, GetBrandResponse>();
        }
    }
}
