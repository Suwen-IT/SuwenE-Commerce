using Application.Features.CQRS.Products.Commands;
using Application.Features.CQRS.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _mediator.Send(new GetAllProductsQueryRequest());
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _mediator.Send(new GetProductByIdQueryRequest { Id = id });
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _mediator.Send(new DeleteProductCommandRequest { Id = id });
           return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
