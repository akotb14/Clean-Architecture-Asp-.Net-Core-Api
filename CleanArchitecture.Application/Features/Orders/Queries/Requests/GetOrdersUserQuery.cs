using CleanArchitecture.Application.Features.Orders.Queries.Responses;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Orders.Queries.Requests
{
    public class GetOrdersUserQuery : IRequest<Response<List<GetOrdersResponse>>>
    {
    }
}
