using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ShoppingCarts.Commads.Requests
{
    public class AddCartItemsCommand : IRequest<Response<string>>
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
    }
}
