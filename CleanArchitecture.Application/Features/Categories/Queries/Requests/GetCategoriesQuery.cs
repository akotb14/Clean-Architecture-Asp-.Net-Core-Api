using CleanArchitecture.Application.Features.Categories.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Categories.Queries.Requests
{
    public class GetCategoriesQuery : IRequest<Response<List<GetCategoryResponse>>>
    {
    }
}
