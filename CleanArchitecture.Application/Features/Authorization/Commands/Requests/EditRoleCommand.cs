using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authorization.Commands.Requests
{
    public class EditRoleCommand : IRequest<Response<string>>
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
