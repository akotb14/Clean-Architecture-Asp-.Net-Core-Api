using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ShoppingCarts.Commads.Requests
{
    public class ClearCartItemsCommand : IRequest<Response<string>>
    {
        public int CartId { get; set; }
    }
}
