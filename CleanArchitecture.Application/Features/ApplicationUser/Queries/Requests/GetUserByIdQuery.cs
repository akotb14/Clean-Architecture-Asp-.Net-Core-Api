using CleanArchitecture.Application.Features.ApplicationUser.Queries.Response;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ApplicationUser.Queries.Requests
{
    public class GetUserByIdQuery : IRequest<Response<GetUserReponseQuery>>
    {
        public string UserId { get; set; }

    }
}
