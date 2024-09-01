using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authorization.Commands.Requests
{
    public class UpdateUserClaimsCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public List<UserClaims> UserClaims { get; set; }
    }
}
