using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Brands.Commands.Requests
{
    public record AddBrandCommad : IRequest<Response<string>>
    {
        public string BrandName { get; set; }
        public string Description { get; set; }
    }
}
