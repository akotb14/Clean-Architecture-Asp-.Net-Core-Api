using CleanArchitecture.Application.Features.Products.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Queries.Requests
{
    public class GetProductByIdQuery : IRequest<Response<GetProductResponse>>
    {
        public int ProductID { get; set; }

    }
}
