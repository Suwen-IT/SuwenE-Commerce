using Application.Common.Models;
using Application.Features.DTOs.Baskets;
using MediatR;

namespace Application.Features.CQRS.Baskets.Commands
{
    public class AddItemToBasketCommandRequest:IRequest<ResponseModel<BasketDto>>
    {
        public Guid AppUserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int? ProductAttributeValueId { get; set; }
    }
}
