using CleanArchitecture.Application.ResultHandler;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Authorization.Queries.Requests
{
    public class GetRoleByIdQuery : IRequest<Response<IdentityRole>>
    {
        public string RoleId { get; set; }
    }
}
