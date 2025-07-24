using Application.Common.Models;
using Application.Features.CQRS.Baskets.Commands;
using Application.Features.DTOs.Baskets;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, ResponseModel<BasketDto>>
    {
        private readonly IReadRepository<Basket> _basketReadRepository;
        private readonly IWriteRepository<Basket> _basketWriteRepository;
        private readonly IWriteRepository<BasketItem> _basketItemWriteRepository;
        private readonly IReadRepository<Product> _productReadRepository;
        private readonly IReadRepository<ProductAttributeValue> _productAttributeValueReadRepository;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public AddItemToBasketCommandHandler(IReadRepository<Basket> basketReadRepository, IWriteRepository<Basket> basketWriteRepository,
            IWriteRepository<BasketItem> basketItemWriteRepository, IReadRepository<Product> productReadRepository, 
            IReadRepository<ProductAttributeValue> productAttributeValueReadRepository,IReservationService reservationService, IMapper mapper)
        {
            _basketReadRepository = basketReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _productReadRepository = productReadRepository;
            _productAttributeValueReadRepository = productAttributeValueReadRepository;
            _reservationService = reservationService;
            _mapper = mapper;
        }
        public async Task<ResponseModel<BasketDto>> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var product=await _productReadRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                return new ResponseModel<BasketDto>("Ürün bulunamadı.", 404);

            if (request.ProductAttributeValueId.HasValue)
            {
                var pav= await _productAttributeValueReadRepository.GetByIdAsync(request.ProductAttributeValueId.Value);
                if (pav == null)
                    return new ResponseModel<BasketDto>("Ürün değer niteliği bulunamadı.", 404);
            }

            var existingBasket = await _basketReadRepository.GetAsync(
                x => x.AppUserId == request.AppUserId,
                include: q => q.Include(b => b.BasketItems));

            if (existingBasket == null)
            {
                existingBasket = new Basket
                {
                    AppUserId = request.AppUserId,
                    CreatedDate = DateTime.UtcNow
                };

                await _basketWriteRepository.AddAsync(existingBasket);
                await _basketWriteRepository.SaveChangesAsync();
            }

            var existingItem=existingBasket.BasketItems?.FirstOrDefault(x=>
                x.ProductId == request.ProductId && 
                x.ProductAttributeValueId == request.ProductAttributeValueId);

            if (existingItem != null)
            {
              
                var quantityDifference = request.Quantity;

                var reserveSuccess = await _reservationService.ReserveStockAsync(
                    request.ProductAttributeValueId.Value,
                    quantityDifference);

                if (!reserveSuccess)
                {
                    return new ResponseModel<BasketDto>("Stok yetersiz. Sepete ekleme işlemi başarısız.", 400);
                }

                existingItem.Quantity += quantityDifference;
                existingItem.UpdatedDate = DateTime.UtcNow;
                existingItem.ReservationExpirationDate = DateTime.UtcNow.AddMinutes(30);

                await _basketItemWriteRepository.UpdateAsync(existingItem);
                await _basketItemWriteRepository.SaveChangesAsync();
            }
            else
            {
               
                bool reservationSuccess = await _reservationService.ReserveStockAsync(
                    request.ProductAttributeValueId.Value,
                    request.Quantity);

                if (!reservationSuccess)
                {
                    return new ResponseModel<BasketDto>("Stok yetersiz. Sepete ekleme işlemi başarısız.", 400);
                }

                var newItem = new BasketItem
                {
                    BasketId = existingBasket.Id,
                    ProductId = request.ProductId,
                    ProductAttributeValueId = request.ProductAttributeValueId,
                    Quantity = request.Quantity,
                    UnitPrice = product.Price,
                    ReservationExpirationDate = DateTime.UtcNow.AddMinutes(30)
                };

                await _basketItemWriteRepository.AddAsync(newItem);
                await _basketItemWriteRepository.SaveChangesAsync();
            }

            var updatedBasket = await _basketReadRepository.GetAsync(
                x => x.Id == existingBasket.Id,
                include: q => q.Include(b => b.BasketItems));

            var basketDto = _mapper.Map<BasketDto>(updatedBasket);
            return new ResponseModel<BasketDto>(basketDto, 200);

        }
    }
}
