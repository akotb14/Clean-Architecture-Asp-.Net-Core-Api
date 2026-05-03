using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ShoppingCarts.Commads.Requests
{
    public class RemoveCartItemCommand : IRequest<Response<string>>
    {
        public int CartItemId { get; set; }
    }
}
