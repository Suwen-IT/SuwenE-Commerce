using Application.Common.Models;
using Application.Features.CQRS.Baskets.Commands;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IReadRepository<BasketItem> _basketItemReadRepository;
        private readonly IWriteRepository<BasketItem> _basketItemWriteRepository;
        private readonly IReservationService _reservationService;

        public DeleteBasketItemCommandHandler(IReadRepository<BasketItem> basketItemReadRepository, IWriteRepository<BasketItem> basketItemWriteRepository,
            IReservationService reservationService)
        {
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _reservationService = reservationService;
        }
        public async Task<ResponseModel<NoContent>> Handle(DeleteBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            var basketItem = await _basketItemReadRepository.GetAsync(
                bi => bi.Id == request.BasketItemId,
                include: q => q.Include(bi => bi.Basket));

            if(basketItem == null||basketItem.Basket==null)
                return new ResponseModel<NoContent>("Sepet öğesi bulunamadı.", 404);

            if (basketItem.Basket.AppUserId != request.AppUserId)
                return new ResponseModel<NoContent>("Bu sepet öğesine erişim izniniz yok.", 403);

            if (basketItem.ProductAttributeValueId.HasValue)
            {
                await _reservationService.ReleaseStockAsync(
                    basketItem.ProductAttributeValueId.Value,
                    basketItem.Quantity
                );
            }

            await _basketItemWriteRepository.DeleteAsync(basketItem);
            await _basketItemWriteRepository.SaveChangesAsync();

            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
    
}
