using Application.Features.CQRS.ProductAttributeValues.Commands;
using Application.Features.CQRS.ProductAttributeValues.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeValuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductAttributeValuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllProductAttributeValuesQueryRequest());
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetProductAttributeValueByIdQueryRequest(id));
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProductAttributeValueCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductAttributeValueCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete(("delete/{id}"))]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteProductAttributeValueCommandRequest(id));
            return StatusCode(response.StatusCode, response);
        }
    }
}