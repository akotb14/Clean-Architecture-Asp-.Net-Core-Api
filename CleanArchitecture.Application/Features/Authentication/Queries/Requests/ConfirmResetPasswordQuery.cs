using CleanArchitecture.Application.ResultHandler;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.Features.Authentication.Queries.Requests
{
    public class ConfirmResetPasswordQuery : IRequest<Response<string>>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
