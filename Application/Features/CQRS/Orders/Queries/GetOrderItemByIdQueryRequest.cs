using Application.Common.Models;
using Application.Features.DTOs.Orders;
using MediatR;

namespace Application.Features.CQRS.Orders.Queries
{
    public class GetOrderItemByIdQueryRequest:IRequest<ResponseModel<List<OrderItemDto>>>
    {
        public int Id { get; set; }
    }
}
