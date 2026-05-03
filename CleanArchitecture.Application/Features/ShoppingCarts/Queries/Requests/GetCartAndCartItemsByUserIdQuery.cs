using CleanArchitecture.Application.Features.ShoppingCarts.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ShoppingCarts.Queries.Requests
{
    public class GetCartAndCartItemsByUserIdQuery : IRequest<Response<List<GetCartandItemsResponse>>>
    {
    }
}
