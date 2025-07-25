using Application.Common.Models;
using Application.Features.CQRS.Baskets.Commands;
using Application.Features.DTOs.Baskets;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.Baskets;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Baskets.Handlers
{
    public class UpdateBasketItemCommandHandler : IRequestHandler<UpdateBasketItemCommandRequest, ResponseModel<BasketDto>>
    {
        private readonly IReadRepository<BasketItem> _basketItemReadRepository;
        private readonly IWriteRepository<BasketItem> _basketItemWriteRepository;
        private readonly IReadRepository<Product> _productReadRepository;
        private readonly IReadRepository<ProductAttributeValue> _productAttributeValueReadRepository;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public UpdateBasketItemCommandHandler(IReadRepository<BasketItem> basketItemReadRepository, IWriteRepository<BasketItem> basketItemWriteRepository,
            IReadRepository<Product> productReadRepository, IReadRepository<ProductAttributeValue> productAttributeValueReadRepository, 
            IReservationService reservationService, IMapper mapper)
        {
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _productReadRepository = productReadRepository;
            _productAttributeValueReadRepository = productAttributeValueReadRepository;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        public async Task<ResponseModel<BasketDto>> Handle(UpdateBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            var basketItem = await _basketItemReadRepository.GetAsync(
            bi => bi.Id == request.BasketItemId,
            include: q => q.Include(bi => bi.Basket).ThenInclude(b => b.BasketItems),
            enableTracking: true);

            if (basketItem == null || basketItem.Basket == null)
                return new ResponseModel<BasketDto>("Sepet ürünü bulunamadı.", 404);

            if (basketItem.Basket.AppUserId != request.AppUserId)
                return new ResponseModel<BasketDto>("Bu sepete erişim izniniz yok.", 403);

            if (request.Quantity <= 0)
                return new ResponseModel<BasketDto>("Adet 0'dan büyük olmalıdır.", 400);

            var product = await _productReadRepository.GetByIdAsync(basketItem.ProductId);
            if (product == null)
                return new ResponseModel<BasketDto>("Ürün bulunamadı.", 404);

            var quantityDifference = request.Quantity - basketItem.Quantity;

            
            if (quantityDifference == 0)
            {
                var dtoNoChange = _mapper.Map<BasketDto>(basketItem.Basket);
                return new ResponseModel<BasketDto>(dtoNoChange, 200);
            }

           
            if (basketItem.ProductAttributeValueId.HasValue)
            {
                var pavId = basketItem.ProductAttributeValueId.Value;
                var pav = await _productAttributeValueReadRepository.GetByIdAsync(pavId);

                if (pav == null)
                    return new ResponseModel<BasketDto>("Ürün niteliği bulunamadı.", 404);

                if (quantityDifference > 0)
                {
                    var reserved = await _reservationService.ReserveStockAsync(pavId, quantityDifference);
                    if (!reserved)
                        return new ResponseModel<BasketDto>("Yeterli stok bulunamadı.", 400);
                }
                else
                {
                    await _reservationService.ReleaseStockAsync(pavId, -quantityDifference);
                }
            }

            basketItem.Quantity = request.Quantity;
            basketItem.UpdatedDate = DateTime.UtcNow;

            await _basketItemWriteRepository.UpdateAsync(basketItem);
            await _basketItemWriteRepository.SaveChangesAsync();

            var basketDto = _mapper.Map<BasketDto>(basketItem.Basket);
            return new ResponseModel<BasketDto>(basketDto, 200);
        }

    }

}
