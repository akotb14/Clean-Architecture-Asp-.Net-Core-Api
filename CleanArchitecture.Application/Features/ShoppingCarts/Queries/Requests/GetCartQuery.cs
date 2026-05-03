using CleanArchitecture.Application.ResultHandler;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.ShoppingCarts.Queries.Requests
{
    public class GetCartQuery : IRequest<Response<ShoppingCart>>
    {
    }
}
