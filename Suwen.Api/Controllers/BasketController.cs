using Application.Features.CQRS.Baskets.Commands;
using Application.Features.CQRS.Baskets.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItemToBasket([FromBody] AddItemToBasketCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBasketItem([FromBody] UpdateBasketItemCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBasketItem([FromBody] DeleteBasketItemCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearBasket([FromBody] ClearBasketCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-by-user/{userId}")]
        public async Task<IActionResult> GetBasketByUserId(Guid userId)
        {
            var result = await _mediator.Send(new GetBasketByUserIdQueryRequest { AppUserId = userId });
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-by-id/{basketId}")]
        public async Task<IActionResult> GetBasketById(int basketId)
        {
            var result = await _mediator.Send(new GetBasketByIdQueryRequest { BasketId = basketId });
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("summary/{userId}")]
        public async Task<IActionResult> GetBasketSummary(Guid userId)
        {
            var result = await _mediator.Send(new GetBasketSummaryQueryRequest { AppUserId = userId });
            return StatusCode(result.StatusCode, result);
        }
    }
}
