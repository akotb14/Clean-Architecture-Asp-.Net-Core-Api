using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Categories.Commands.Requests
{
    public record EditCategoryCommad : IRequest<Response<string>>
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryID { get; set; }
        public string Description { get; set; }
    }
}
