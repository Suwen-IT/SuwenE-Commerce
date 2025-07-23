using Application.Common.Models;
using Application.Features.DTOs.Baskets;
using MediatR;

namespace Application.Features.CQRS.Baskets.Queries
{
    public class GetBasketByUserIdQueryRequest:IRequest<ResponseModel<BasketDto>>
    {
        public Guid AppUserId { get; set; }
    }
}
