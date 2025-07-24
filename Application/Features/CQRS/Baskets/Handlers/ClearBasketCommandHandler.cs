using Application.Common.Models;
using Application.Features.CQRS.Baskets.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class ClearBasketCommandHandler : IRequestHandler<ClearBasketCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IReadRepository<Basket> _basketReadRepository;
        private readonly IWriteRepository<BasketItem> _basketItemWriteRepository;

        public ClearBasketCommandHandler( IReadRepository<Basket> basketReadRepository, IWriteRepository<BasketItem> basketItemWriteRepository)
        {
            _basketReadRepository = basketReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
        }

        public async Task<ResponseModel<NoContent>> Handle(ClearBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var basket = await _basketReadRepository.GetAsync(
                b => b.AppUserId == request.AppUserId,
                include: b => b.Include(b => b.BasketItems));

            if (basket == null)
                return new ResponseModel<NoContent>("Kullanıcıya ait sepet bulunamadı.",404);

            if(basket.BasketItems== null || !basket.BasketItems.Any())
                return new ResponseModel<NoContent>("Sepet zaten boş.", 200);

            foreach (var item in basket.BasketItems)
            {
                await _basketItemWriteRepository.DeleteAsync(item);
            }

            await _basketItemWriteRepository.SaveChangesAsync();
            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
}
