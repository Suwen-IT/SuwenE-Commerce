using Application.Common.Models;
using Application.Features.DTOs.Orders;
using MediatR;

namespace Application.Features.CQRS.Orders.Queries
{
    public class GetOrderByIdQueryRequest:IRequest<ResponseModel<OrderDto>>
    {
        public int Id { get; set; }
        public Guid AppUserId { get; set; }
    }
}
