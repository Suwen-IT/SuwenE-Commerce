using Application.Features.CQRS.Notifications.Commands;
using Application.Features.CQRS.Notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Suwen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public NotificationController(IMediator mediator)
        { 
             _mediator = mediator;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetNotifications([FromQuery] Guid userId)
        {
            var query = new GetNotificationsQueryRequest { UserId = userId };
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("markasread")]
        public async Task<IActionResult> MarkAsRead([FromQuery] int notificationId)
        {
            var command = new MarkAsReadCommandRequest { NotificationId = notificationId };
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }
    }
}
