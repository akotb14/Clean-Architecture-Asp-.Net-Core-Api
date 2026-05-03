using CleanArchitecture.Api.Base;
using CleanArchitecture.Application.Features.Products.Commands.Requests;
using CleanArchitecture.Application.Features.Products.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProductsQuery query)
        {
            var res = await _mediator.Send(query);
            return Ok(res);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await _mediator.Send(new GetProductByIdQuery() { ProductID = id });
            return NewResult(res);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddProductCommad commad)
        {
            var res = await _mediator.Send(commad);
            return NewResult(res);
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditProductCommad commad)
        {
            var res = await _mediator.Send(commad);
            return NewResult(res);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await _mediator.Send(new DeleteProductCommad() { ProductID = id });
            return NewResult(res);
        }
    }
}
