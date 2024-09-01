using CleanArchitecture.Application.Features.Authorization.Queries.Response;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authorization.Queries.Requests
{
    public class GetUserClaimsQuery : IRequest<Response<GetUserClaimsResponse>>
    {
        public string UserId { get; set; }
    }
}
