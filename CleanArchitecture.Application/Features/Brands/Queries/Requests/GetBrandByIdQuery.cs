using CleanArchitecture.Application.Features.Brands.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Brands.Queries.Requests
{
    public class GetBrandByIdQuery : IRequest<Response<GetBrandResponse>>
    {
        public int BrandID { get; set; }

    }
}
