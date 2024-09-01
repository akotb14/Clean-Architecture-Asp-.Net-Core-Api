using CleanArchitecture.Api.Base;
using CleanArchitecture.Application.Features.ApplicationUser.Commands.Requests;
using CleanArchitecture.Application.Features.ApplicationUser.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ApplicatioUserController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicatioUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUserPaginationQuery query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var res = await _mediator.Send(new GetUserByIdQuery() { UserId = id });
            return NewResult(res);
        }
        [HttpPost("create-user")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(AddUserCommand command)
        {

            var res = await _mediator.Send(command);
            return NewResult(res);
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }
        [HttpPut("upload-image")]
        public async Task<IActionResult> UploadImage([FromForm] EditImageProfileRequest command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            var res = await _mediator.Send(new DeleteUserCommand() { UserId = userId });
            return NewResult(res);
        }
    }
}
