using CleanArchitecture.Api.Base;
using CleanArchitecture.Application.Features.ShoppingCarts.Commads.Requests;
using CleanArchitecture.Application.Features.ShoppingCarts.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> createCart()
        {
            var res = await _mediator.Send(new AddCartCommand());
            return NewResult(res);
        }
        [HttpPost("create-item")]
        public async Task<IActionResult> createCartItems(AddCartItemsCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }
        [HttpPut("edit-item")]
        public async Task<IActionResult> EditCartItems(UpdateCartItemsCommand command)
        {
            var res = await _mediator.Send(command);
            return NewResult(res);
        }
        [HttpDelete("remove-item/{id}")]
        public async Task<IActionResult> RemoveCartItems([FromRoute] int id)
        {
            var res = await _mediator.Send(new RemoveCartItemCommand() { CartItemId = id });
            return NewResult(res);
        }
        [HttpDelete("clear-cartItems")]
        public async Task<IActionResult> ClearCartItems()
        {
            var res = await _mediator.Send(new ClearCartItemsCommand());
            return NewResult(res);
        }
        [HttpGet("cart-items")]
        public async Task<IActionResult> GetCartAndCartItems()
        {
            var res = await _mediator.Send(new GetCartAndCartItemsByUserIdQuery());
            return NewResult(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetCartQuery()
        {
            var res = await _mediator.Send(new GetCartQuery());
            return NewResult(res);
        }
        [HttpGet("total-quantity")]
        public async Task<IActionResult> GeTotalQuantityQuery()
        {
            var res = await _mediator.Send(new GetTotalQuantityQuery());
            return NewResult(res);
        }
    }
}
