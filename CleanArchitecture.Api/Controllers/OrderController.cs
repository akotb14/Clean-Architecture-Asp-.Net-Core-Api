using CleanArchitecture.Api.Base;
using CleanArchitecture.Application.Features.Orders.Commands.Requests;
using CleanArchitecture.Application.Features.Orders.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> createOrder(CreateOrderCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }
        [AllowAnonymous]
        [HttpPost("state")]
        public async Task<IActionResult> PaymentState([FromBody] OrderCallbackCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _mediator.Send(command);
            return Ok(res);
        }
        [AllowAnonymous]
        [HttpGet("state")]
        public async Task<IActionResult> getPaymentState([FromQuery] string success)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string frontendUrl = $"http://localhost:5173/orders?success={success}";

            return Redirect(frontendUrl);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> getOrdersUserState()
        {
            var res = await _mediator.Send(new GetOrdersUserQuery());
            return NewResult(res);
        }
    }


}
