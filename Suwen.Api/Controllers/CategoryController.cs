using Application.Features.CQRS.Categories.Commands;
using Application.Features.CQRS.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]

        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoyCommandRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.StatusCode == 200)
                return Ok(response);
            return BadRequest(response);
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _mediator.Send(new GetAllCategoriesQueryRequest());
            if (response.StatusCode == 200)
                return Ok(response);
            return NotFound(response);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await _mediator.Send(new GetCategoryByIdQueryRequest { Id = id });
            if (response.StatusCode == 200)
                return Ok(response);
            return NotFound(response);
        }

        [HttpDelete("delete/{id}")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _mediator.Send(new DeleteCategoryCommandRequest { Id = id });
            if (response.StatusCode == 200)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommandRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.StatusCode == 200)
                return Ok(response);
            return BadRequest(response);
        }
    }

}