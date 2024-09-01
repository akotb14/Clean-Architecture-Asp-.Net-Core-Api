using CleanArchitecture.Api.Base;
using CleanArchitecture.Application.Features.Authorization.Commands.Requests;
using CleanArchitecture.Application.Features.Authorization.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var res = await _mediator.Send(new GetRolesQuery());
            return NewResult(res);
        }
        [HttpGet("role/{id}")]
        public async Task<IActionResult> GetRoles([FromRoute] string id)
        {
            var res = await _mediator.Send(new GetRoleByIdQuery() { RoleId = id });
            return NewResult(res);
        }
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(AddRoleCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }
        [HttpDelete("delete-role/{id}")]
        public async Task<IActionResult> DeleteRoles([FromRoute] string id)
        {
            var res = await _mediator.Send(new DeleteRoleCommand() { RoleId = id });
            return NewResult(res);
        }
        [HttpPut("edit-role")]
        public async Task<IActionResult> EditRoles(EditRoleCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }
        [HttpPut("updateUser-roles")]
        public async Task<IActionResult> EditUserRoles(UpdateUserRoleCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }
        [HttpGet("getManageUserRoles/{id}")]
        public async Task<IActionResult> GetEditUserRoles(string id)
        {
            var res = await _mediator.Send(new GetUserRolesQuery() { UserId = id });
            return NewResult(res);
        }
        [HttpGet("getManageUserClaims/{id}")]
        public async Task<IActionResult> GetEditUserClaims(string id)
        {
            var res = await _mediator.Send(new GetUserClaimsQuery() { UserId = id });
            return NewResult(res);
        }
        [HttpPut("updateUser-claims")]
        public async Task<IActionResult> EditUserClaims(UpdateUserClaimsCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }
    }
}
