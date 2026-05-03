using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ShoppingCarts.Commads.Requests
{
    public class AddCartCommand : IRequest<Response<string>>
    {
    }
}
