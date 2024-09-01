using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authentication.Commands.Requests
{
    public record SendResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
