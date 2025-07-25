using Application.Common.Models;
using Application.Features.DTOs.Orders;
using Domain.Entities.Enums;
using MediatR;

namespace Application.Features.CQRS.Orders.Commands
{
    public class UpdateOrderStatusCommandRequest:IRequest<ResponseModel<OrderDto>>
    {
        public int OrderId { get; set; }
        public Guid AppUserId { get; set; }
        public OrderStatus NewStatus { get; set; }
    }
}
