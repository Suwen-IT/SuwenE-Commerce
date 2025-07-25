using Application.Common.Models;
using Application.Features.CQRS.Orders.Commands;
using Application.Features.DTOs.Orders;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Baskets;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.CQRS.Orders.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, ResponseModel<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IReservationService reservationService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        public async Task<ResponseModel<OrderDto>> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var basketRepo = _unitOfWork.GetReadRepository<Basket>();
            var basket = await basketRepo.GetAsync(
                b => b.AppUserId == request.AppUserId,
                include: q => q.Include(b => b.BasketItems),
                enableTracking: true);

            if (basket == null || basket.BasketItems == null || !basket.BasketItems.Any())
                return new ResponseModel<OrderDto>("Sepette ürün bulunamadı.", 400);

            if (request.ShippingAddressId == null)
                return new ResponseModel<OrderDto>("Teslimat adresi boş olamaz.", 400);

            if (request.BillingAddressId == null)
                return new ResponseModel<OrderDto>("Fatura adresi boş olamaz.", 400);

            var addressRepo = _unitOfWork.GetReadRepository<Address>();
            var shippingAddress = await addressRepo.GetByIdAsync(request.ShippingAddressId);
            var billingAddress = await addressRepo.GetByIdAsync(request.BillingAddressId.Value);

            if (shippingAddress == null || billingAddress == null)
                return new ResponseModel<OrderDto>("Adres bilgileri geçersiz.", 400);

            var order = _mapper.Map<Order>(request);
            order.ShippingAddress = shippingAddress;
            order.BillingAddress = billingAddress;
            order.OrderDate = DateTime.UtcNow;
            order.OrderStatus = Domain.Entities.Enums.OrderStatus.Beklemede;


            var orderItems = basket.BasketItems.Select(bi => new OrderItem
            {
                ProductId = bi.ProductId,
                ProductAttributeValueId = bi.ProductAttributeValueId,
                Quantity = bi.Quantity,
                UnitPrice = bi.UnitPrice,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }).ToList();

            order.OrderItems = orderItems;

            var orderWriteRepo = _unitOfWork.GetWriteRepository<Order>();
            var pavReadRepo = _unitOfWork.GetReadRepository<ProductAttributeValue>();
            var pavWriteRepo = _unitOfWork.GetWriteRepository<ProductAttributeValue>();
            var basketWriteRepo = _unitOfWork.GetWriteRepository<Basket>();

            foreach (var item in basket.BasketItems)
            {
                var pav = await pavReadRepo.GetByIdAsync(item.ProductAttributeValueId!.Value);
                if (pav == null)
                    return new ResponseModel<OrderDto>($"Ürün özelliği bulunamadı: {item.ProductAttributeValueId}", 400);

                pav.Stock -= item.Quantity;
                await pavWriteRepo.UpdateAsync(pav);
            }

            foreach (var item in basket.BasketItems)
            {
                if (item.ProductAttributeValueId.HasValue)
                    await _reservationService.ReleaseStockAsync(item.ProductAttributeValueId.Value, item.Quantity);
            }


            await orderWriteRepo.AddAsync(order);

            await basketWriteRepo.DeleteAsync(basket);


            var result = await _unitOfWork.SaveChangesAsync();
            if (result <= 0)
                return new ResponseModel<OrderDto>("Sipariş oluşturulurken hata oluştu.", 500);

            var orderDto = _mapper.Map<OrderDto>(order);
            return new ResponseModel<OrderDto>(orderDto, 201);
        }
    }
}
