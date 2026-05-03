using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ShoppingCarts.Commads.Requests
{
    public class UpdateCartItemsCommand : IRequest<Response<string>>
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }

    }
}
