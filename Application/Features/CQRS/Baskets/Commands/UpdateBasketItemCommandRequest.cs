using Application.Common.Models;
using Application.Features.DTOs.Baskets;
using MediatR;

namespace Application.Features.CQRS.Baskets.Commands
{
    public class UpdateBasketItemCommandRequest:IRequest<ResponseModel<BasketDto>>
    {
        public int BasketItemId { get; set; }
        public int Quantity { get; set; }
        public Guid AppUserId { get; set; }
      
    }
    
}
