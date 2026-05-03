using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Commands.Requests
{
    public record DeleteProductCommad : IRequest<Response<string>>
    {
        public int ProductID { get; set; }
    }
}
