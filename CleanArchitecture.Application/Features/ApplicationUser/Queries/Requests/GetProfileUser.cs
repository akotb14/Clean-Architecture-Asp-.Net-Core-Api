using CleanArchitecture.Application.Features.ApplicationUser.Queries.Response;
using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ApplicationUser.Queries.Requests
{
    public class GetProfileUserQuery : IRequest<Response<GetUserReponseQuery>>
    {

    }
}
