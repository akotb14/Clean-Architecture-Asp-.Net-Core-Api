using CleanArchitecture.Api.Base;
using CleanArchitecture.Application.Features.Authentication.Commands.Requests;
using CleanArchitecture.Application.Features.Authentication.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInCommand command)
        {


            var res = await _mediator.Send(command);
            return NewResult(res);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var res = await _mediator.Send(query);
            return NewResult(res);
        }
        [HttpGet("ConfirmResetPassword")]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery query)
        {
            var res = await _mediator.Send(query);
            return NewResult(res);
        }
        [HttpGet("valid-token")]
        public async Task<IActionResult> ValidToken([FromQuery] AuthorizeUserQuery query)
        {
            var res = await _mediator.Send(query);
            return NewResult(res);
        }
        [HttpPost("send-reset-password")]
        public async Task<IActionResult> SendResetPassword(SendResetPasswordCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }
        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command, [FromQuery] string code)
        {
            command.Code = code;
            var res = await _mediator.Send(command);
            return NewResult(res);
        }

    }
}
