using Application.Common.Models;
using MediatR;

namespace Application.Features.CQRS.Orders.Commands
{
    public class CancelOrderCommandRequest:IRequest<ResponseModel<NoContent>>
    {
        public int OrderId { get; set; }
        public Guid AppUserId { get; set; }
    }
}
