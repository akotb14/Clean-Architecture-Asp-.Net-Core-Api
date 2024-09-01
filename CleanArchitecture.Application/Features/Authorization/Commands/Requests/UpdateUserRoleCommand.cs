using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authorization.Commands.Requests
{
    public class UpdateUserRoleCommand : UserRolesResult, IRequest<Response<string>>
    {
    }
}
