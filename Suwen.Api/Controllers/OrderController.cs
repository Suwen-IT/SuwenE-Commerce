using Application.Features.CQRS.Orders.Commands;
using Application.Features.CQRS.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id, [FromQuery] Guid appUserId)
        {
            var result = await _mediator.Send(new DeleteOrderCommandRequest { Id = id, AppUserId = appUserId });
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOrderById(int id, [FromQuery] Guid appUserId)
        {
            var result = await _mediator.Send(new GetOrderByIdQueryRequest { Id = id, AppUserId = appUserId });
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("user-orders")]
        public async Task<IActionResult> GetAllOrdersByUser([FromQuery] GetAllOrdersByUserIdQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetOrderItemsByOrderId(int id)
        {
            var result = await _mediator.Send(new GetOrderItemByIdQueryRequest { Id = id });
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId, [FromQuery] Guid appUserId)
        {
            var result = await _mediator.Send(new CancelOrderCommandRequest { OrderId = orderId, AppUserId=appUserId});
            return StatusCode(result.StatusCode, result);
        }
    }
}
