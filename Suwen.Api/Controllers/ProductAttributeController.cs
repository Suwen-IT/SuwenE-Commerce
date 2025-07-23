using Application.Features.CQRS.ProductAttributes.Commands;
using Application.Features.CQRS.ProductAttributes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductAttributeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductAttribute([FromBody] CreateProductAttributeCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetProductAttributes()
        {
            var response = await _mediator.Send(new GetAllProductAttributesQueryRequest());
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetProductAttributeById(int id)
        {
            var response = await _mediator.Send(new GetProductAttributeByIdQueryRequest(id));
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProductAttribute(int id)
        {
            var response = await _mediator.Send(new DeleteProductAttributeCommandRequest(id));
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProductAttribute([FromBody] UpdateProductAttributeCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

    }
}
