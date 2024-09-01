using CleanArchitecture.Application.ResultHandler;
using MediatR;

namespace CleanArchitecture.Application.Features.Authentication.Queries.Requests
{
    public record ConfirmEmailQuery : IRequest<Response<string>>
    {
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
