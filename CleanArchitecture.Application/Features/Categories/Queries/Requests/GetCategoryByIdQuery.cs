using CleanArchitecture.Application.Features.Categories.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Categories.Queries.Requests
{
    public class GetCategoryByIdQuery : IRequest<Response<GetCategoryResponse>>
    {
        public int CategoryID { get; set; }

    }
}
