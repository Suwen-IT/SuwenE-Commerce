using Application.Common.Models;
using Application.Features.DTOs.Baskets;
using MediatR;

namespace Application.Features.CQRS.Baskets.Queries
{
    public class GetBasketSummaryQueryRequest:IRequest<ResponseModel<BasketSummaryDto>>
    {
        public Guid AppUserId { get; set; }
    }
}
