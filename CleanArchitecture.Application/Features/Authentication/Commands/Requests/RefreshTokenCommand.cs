using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authentication.Commands.Requests
{
    public record RefreshTokenCommand : IRequest<Response<AuthJwtResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
