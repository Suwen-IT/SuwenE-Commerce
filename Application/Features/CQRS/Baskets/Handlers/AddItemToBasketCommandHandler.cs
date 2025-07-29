using Application.Common.Models;
using Application.Features.CQRS.Baskets.Commands;
using Application.Features.DTOs.Baskets;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Baskets;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, ResponseModel<BasketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public AddItemToBasketCommandHandler(IUnitOfWork unitOfWork, IReservationService reservationService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        public async Task<ResponseModel<BasketDto>> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetReadRepository<Product>().GetByIdAsync(request.ProductId, tracking: false);
            if (product == null)
                return new ResponseModel<BasketDto>("Ürün bulunamadı.", 404);

            if (request.ProductAttributeValueId.HasValue)
            {
                var pav = await _unitOfWork.GetReadRepository<ProductAttributeValue>().GetByIdAsync(request.ProductAttributeValueId.Value, tracking: false);
                if (pav == null)
                    return new ResponseModel<BasketDto>("Ürün değer niteliği bulunamadı.", 404);
            }

            var existingBasket = await _unitOfWork.GetReadRepository<Basket>()
                .GetAsync(b => b.AppUserId == request.AppUserId, include: q => q.Include(b => b.BasketItems), enableTracking: true);

            if (existingBasket == null)
            {
                existingBasket = new Basket
                {
                    AppUserId = request.AppUserId,
                    CreatedDate = DateTime.UtcNow
                };

                await _unitOfWork.GetWriteRepository<Basket>().AddAsync(existingBasket);
            }

            var existingItem = existingBasket.BasketItems?.FirstOrDefault(x =>
                x.ProductId == request.ProductId &&
                x.ProductAttributeValueId == request.ProductAttributeValueId);

            if (existingItem != null)
            {
                var reserveSuccess = await _reservationService.ReserveStockAsync(
                    request.ProductAttributeValueId ?? default, 
                    request.Quantity);

                if (!reserveSuccess)
                    return new ResponseModel<BasketDto>("Stok yetersiz. Sepete ekleme işlemi başarısız.", 400);

                existingItem.Quantity += request.Quantity;
                existingItem.UpdatedDate = DateTime.UtcNow;
                existingItem.ReservationExpirationDate = DateTime.UtcNow.AddMinutes(30);

                await _unitOfWork.GetWriteRepository<BasketItem>().UpdateAsync(existingItem);
            }
            else
            {
                var reservationSuccess = await _reservationService.ReserveStockAsync(
                    request.ProductAttributeValueId ?? default,
                    request.Quantity);

                if (!reservationSuccess)
                    return new ResponseModel<BasketDto>("Stok yetersiz. Sepete ekleme işlemi başarısız.", 400);

                var newItem = new BasketItem
                {
                    BasketId = existingBasket.Id,
                    ProductId = request.ProductId,
                    ProductAttributeValueId = request.ProductAttributeValueId,
                    Quantity = request.Quantity,
                    UnitPrice = product.Price,
                    ReservationExpirationDate = DateTime.UtcNow.AddMinutes(30)
                };

                await _unitOfWork.GetWriteRepository<BasketItem>().AddAsync(newItem);
            }

            var success = await _unitOfWork.SaveChangesBoolAsync();

            if (!success)
                return new ResponseModel<BasketDto>("Sepet güncellenirken hata oluştu.", 500);

            var updatedBasket = await _unitOfWork.GetReadRepository<Basket>()
                .GetAsync(b => b.Id == existingBasket.Id, include: q => q.Include(b => b.BasketItems), enableTracking: false);

            var basketDto = _mapper.Map<BasketDto>(updatedBasket);
            return new ResponseModel<BasketDto>(basketDto, 200);
        }
    }
}
