using CleanArchitecture.Application.Features.Brands.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Brands.Queries.Requests
{
    public class GetBrandQuery : IRequest<Response<List<GetBrandResponse>>>
    {
    }
}
