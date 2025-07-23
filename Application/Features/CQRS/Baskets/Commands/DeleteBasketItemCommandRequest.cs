using Application.Common.Models;
using MediatR;

namespace Application.Features.CQRS.Baskets.Commands
{
    public class DeleteBasketItemCommandRequest:IRequest<ResponseModel<NoContent>>
    {
        public int BasketItemId { get; set; }
        public Guid AppUserId { get; set; }
    }
}
