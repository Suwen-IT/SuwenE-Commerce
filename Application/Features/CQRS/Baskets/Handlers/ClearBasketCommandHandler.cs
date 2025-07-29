using Application.Common.Models;
using Application.Features.CQRS.Baskets.Commands;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities.Baskets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class ClearBasketCommandHandler : IRequestHandler<ClearBasketCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClearBasketCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<NoContent>> Handle(ClearBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var basket = await _unitOfWork.GetReadRepository<Basket>()
                .GetAsync(
                    b => b.AppUserId == request.AppUserId,
                    include: b => b.Include(b => b.BasketItems),
                    enableTracking: true);

            if (basket == null)
                return new ResponseModel<NoContent>("Kullanıcıya ait sepet bulunamadı.", 404);

            if (basket.BasketItems == null || !basket.BasketItems.Any())
                return new ResponseModel<NoContent>("Sepet zaten boş.", 200);

            var basketItemRepo = _unitOfWork.GetWriteRepository<BasketItem>();

            foreach (var item in basket.BasketItems)
            {
                await basketItemRepo.DeleteAsync(item);
            }

            var success = await _unitOfWork.SaveChangesBoolAsync();

            if (!success)
                return new ResponseModel<NoContent>("Sepet temizlenirken hata oluştu.", 500);

            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
}