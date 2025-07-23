using Application.Features.CQRS.Reviews.Commands;
using Application.Features.CQRS.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost ("Create")]
        public async Task<IActionResult> Create([FromBody] CreateReviewCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateReviewCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] Guid appUserId)
        {
            var response = await _mediator.Send(new DeleteReviewCommandRequest { Id = id, AppUserId = appUserId });
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetReviewByIdQueryRequest { Id = id});
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Get/reviews/{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var response = await _mediator.Send(new GetReviewsByProductIdQueryRequest { ProductId = productId });
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("user/reviews/{appUserId}")]
        public async Task<IActionResult> GetByUserId(Guid appUserId)
        {
            var response = await _mediator.Send(new GetReviewsByUserIdQueryRequest { AppUserId = appUserId });
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("summary/{productId}")]
        public async Task<IActionResult> GetReviewSummary(int productId)
        {
            var response = await _mediator.Send(new GetProductReviewSummaryQueryRequest { ProductId = productId });
            return StatusCode(response.StatusCode, response);
        }

    }
}

