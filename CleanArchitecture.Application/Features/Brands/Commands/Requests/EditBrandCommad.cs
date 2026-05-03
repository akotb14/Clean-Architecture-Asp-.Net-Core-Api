using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Brands.Commands.Requests
{
    public record EditBrandCommad : IRequest<Response<string>>
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
    }
}
