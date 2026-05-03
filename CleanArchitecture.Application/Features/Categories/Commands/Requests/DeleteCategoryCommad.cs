using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Categories.Commands.Requests
{
    public record DeleteCategoryCommad : IRequest<Response<string>>
    {
        public int CategoryID { get; set; }
    }
}
