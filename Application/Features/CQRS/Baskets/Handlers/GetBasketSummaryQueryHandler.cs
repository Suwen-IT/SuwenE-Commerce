using Application.Common.Models;
using Application.Features.CQRS.Baskets.Queries;
using Application.Features.DTOs.Baskets;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class GetBasketSummaryQueryHandler : IRequestHandler<GetBasketSummaryQueryRequest, ResponseModel<BasketSummaryDto>>
    {
        private readonly IReadRepository<Basket> _basketReadRepository;
        public GetBasketSummaryQueryHandler(IReadRepository<Basket> basketReadRepository)
        {
            _basketReadRepository = basketReadRepository;
        }
        public async Task<ResponseModel<BasketSummaryDto>> Handle(GetBasketSummaryQueryRequest request, CancellationToken cancellationToken)
        {
            var basket = await _basketReadRepository.GetAsync(
                b => b.AppUserId == request.AppUserId,
                include: b => b.Include(x => x.BasketItems));

            if (basket == null || basket.BasketItems == null || !basket.BasketItems.Any())
            {
                return new ResponseModel<BasketSummaryDto>("Sepet bulunamadı veya boş.", 404);
            }

            var totalItems = basket.BasketItems.Sum(x => x.Quantity);
            var totalPrice = basket.BasketItems.Sum(x => x.UnitPrice * x.Quantity);

            var summaryDto = new BasketSummaryDto
            {
                TotalItems = totalItems,
                TotalPrice = totalPrice
            };

            return new ResponseModel<BasketSummaryDto>(summaryDto, 200);
        }
    }
}
