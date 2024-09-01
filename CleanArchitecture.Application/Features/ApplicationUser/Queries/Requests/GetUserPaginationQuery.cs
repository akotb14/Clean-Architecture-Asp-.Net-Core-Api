using CleanArchitecture.Application.Features.ApplicationUser.Queries.Response;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ApplicationUser.Queries.Requests
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<GetUserReponseQuery>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? FilterSearch { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderByDirection { get; set; }
    }
}
