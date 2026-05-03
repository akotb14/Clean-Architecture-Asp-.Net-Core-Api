using CleanArchitecture.Api.Base;
using CleanArchitecture.Application.Features.Brands.Commands.Requests;
using CleanArchitecture.Application.Features.Brands.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _mediator.Send(new GetBrandQuery());
            return NewResult(res);
        }

        // GET api/<BrandController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await _mediator.Send(new GetBrandByIdQuery() { BrandID = id });
            return NewResult(res);
        }

        // POST api/<BrandController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBrandCommad commad)
        {
            var res = await _mediator.Send(commad);
            return NewResult(res);
        }

        // PUT api/<BrandController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditBrandCommad commad)
        {
            var res = await _mediator.Send(commad);
            return NewResult(res);
        }

        // DELETE api/<BrandController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await _mediator.Send(new DeleteBrandCommad() { BrandID = id });
            return NewResult(res);
        }
    }
}
