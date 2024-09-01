using CleanArchitecture.Application.ResultHandler;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.ApplicationUser.Commands.Requests
{
    public class EditImageProfileRequest : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public IFormFile ImageProfile { get; set; }
    }
}
