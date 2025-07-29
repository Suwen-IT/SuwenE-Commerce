using Application.Common.Models;
using Application.Features.CQRS.Baskets.Commands;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities.Baskets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationService _reservationService;

        public DeleteBasketItemCommandHandler(IUnitOfWork unitOfWork, IReservationService reservationService)
        {
            _unitOfWork = unitOfWork;
            _reservationService = reservationService;
        }

        public async Task<ResponseModel<NoContent>> Handle(DeleteBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            var basketItem = await _unitOfWork.GetReadRepository<BasketItem>()
                .GetAsync(
                    bi => bi.Id == request.BasketItemId,
                    include: q => q.Include(bi => bi.Basket),
                    enableTracking: true);

            if (basketItem == null || basketItem.Basket == null)
                return new ResponseModel<NoContent>("Sepet öğesi bulunamadı.", 404);

            if (basketItem.Basket.AppUserId != request.AppUserId)
                return new ResponseModel<NoContent>("Bu sepet öğesine erişim izniniz yok.", 403);

            if (basketItem.ProductAttributeValueId.HasValue)
            {
                await _reservationService.ReleaseStockAsync(
                    basketItem.ProductAttributeValueId.Value,
                    basketItem.Quantity);
            }

            await _unitOfWork.GetWriteRepository<BasketItem>().DeleteAsync(basketItem);

            var success = await _unitOfWork.SaveChangesBoolAsync();

            if (!success)
                return new ResponseModel<NoContent>("Sepet öğesi silinirken hata oluştu.", 500);

            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
}
