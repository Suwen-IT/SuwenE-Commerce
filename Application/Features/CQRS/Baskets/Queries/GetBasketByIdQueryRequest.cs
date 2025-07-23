using Application.Common.Models;
using Application.Features.DTOs.Baskets;
using MediatR;

namespace Application.Features.CQRS.Baskets.Queries
{
    public class GetBasketByIdQueryRequest:IRequest<ResponseModel<BasketDto>>
    {
        public int BasketId { get; set; }
    }
}
