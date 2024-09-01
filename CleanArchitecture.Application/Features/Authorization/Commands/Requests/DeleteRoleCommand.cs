using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authorization.Commands.Requests
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        public string RoleId { get; set; }
    }
}
