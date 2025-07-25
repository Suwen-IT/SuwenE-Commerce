using Application.Common.Models;
using Application.Features.DTOs.Orders;
using Application.Interfaces.Validations;
using MediatR;

namespace Application.Features.CQRS.Orders.Commands
{
    public class UpdateOrderCommandRequest:IRequest<ResponseModel<OrderDto>>, IOrderBaseCommand
    {
        public int OrderId { get; set; }
        public Guid AppUserId { get; set; }
        public int ShippingAddressId { get; set; }
        public int? BillingAddressId { get; set; }
    }
}
