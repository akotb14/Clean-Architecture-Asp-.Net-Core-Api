using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ShoppingCarts.Queries.Requests
{
    public record GetTotalQuantityQuery : IRequest<Response<int>>
    {
    }
}
