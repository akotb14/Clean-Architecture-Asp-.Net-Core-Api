using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Categories.Commands.Requests
{
    public record AddCategoryCommad : IRequest<Response<string>>
    {
        public string CategoryName { get; set; }
        public int? ParentCategoryID { get; set; }
        public string Description { get; set; }
    }
}
