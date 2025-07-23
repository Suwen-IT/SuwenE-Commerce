using Application.Common.Models;
using MediatR;

namespace Application.Features.CQRS.Baskets.Commands
{
    public class ClearBasketCommandRequest:IRequest<ResponseModel<NoContent>>
    {
        public Guid AppUserId { get; set; }
    }
}
