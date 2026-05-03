using AutoMapper;
using CleanArchitecture.Application.Features.Categories.Commands.Requests;
using CleanArchitecture.Application.Features.Categories.Queries.Responses;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mapping
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, GetCategoryResponse>();
            CreateMap<AddCategoryCommad, Category>();
            CreateMap<EditCategoryCommad, Category>();


        }
    }
}
