using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authentication.Commands.Requests
{
    public record SignInCommand : IRequest<Response<AuthJwtResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
