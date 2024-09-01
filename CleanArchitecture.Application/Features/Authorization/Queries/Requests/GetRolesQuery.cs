using CleanArchitecture.Application.ResultHandler;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Authorization.Queries.Requests
{
    public class GetRolesQuery : IRequest<Response<List<IdentityRole>>>
    {
    }
}
