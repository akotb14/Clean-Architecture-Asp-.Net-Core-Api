using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Brands.Commands.Requests
{
    public record DeleteBrandCommad : IRequest<Response<string>>
    {
        public int BrandID { get; set; }

    }
}
