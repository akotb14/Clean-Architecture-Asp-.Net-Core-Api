using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authorization.Commands.Requests
{
    public record AddRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}
