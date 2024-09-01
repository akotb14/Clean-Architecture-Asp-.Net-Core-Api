using CleanArchitecture.Application.ResultHandler;
using MediatR;
using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.Features.Authentication.Commands.Requests
{
    public record ResetPasswordCommand : IRequest<Response<string>>
    {
        [JsonIgnore]
        public string? Code { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
