using CleanArchitecture.Api.Base;
using CleanArchitecture.Application.Features.Categories.Commands.Requests;
using CleanArchitecture.Application.Features.Categories.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _mediator.Send(new GetCategoriesQuery());
            return NewResult(res);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await _mediator.Send(new GetCategoryByIdQuery() { CategoryID = id });
            return NewResult(res);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCategoryCommad commad)
        {
            var res = await _mediator.Send(commad);
            return NewResult(res);
        }

        // PUT api/<CategoryController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditCategoryCommad commad)
        {
            var res = await _mediator.Send(commad);
            return NewResult(res);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await _mediator.Send(new DeleteCategoryCommad() { CategoryID = id });
            return NewResult(res);
        }
    }
}
