using CleanArchitecture.Application.Features.Authorization.Queries.Response;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authorization.Queries.Requests
{
    public record GetUserRolesQuery : IRequest<Response<GetUserRolesResponse>>
    {
        public string UserId { get; set; }
    }
}
