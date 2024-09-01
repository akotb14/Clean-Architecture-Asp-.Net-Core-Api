using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.ApplicationUser.Commands.Requests
{
    public record DeleteUserCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
    }
}
