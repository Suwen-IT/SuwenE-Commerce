using Application.Common.Models;
using Application.Features.CQRS.Orders.Commands;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Orders.Handlers
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IReadRepository<Order> _orderReadRepository;
        private readonly IWriteRepository<Order> _orderWriteRepository;
        private readonly IReservationService _reservationService;

        public CancelOrderCommandHandler(
            IReadRepository<Order> orderReadRepository,
            IWriteRepository<Order> orderWriteRepository,
            IReservationService reservationService)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _reservationService = reservationService;
        }

        public async Task<ResponseModel<NoContent>> Handle(CancelOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderReadRepository.GetAsync(
                o => o.Id == request.OrderId && o.AppUserId == request.AppUserId,
                include: o => o.Include(o => o.OrderItems),
                enableTracking: true);

            if (order == null)
                return new ResponseModel<NoContent>("Sipariş bulunamadı.", 404);

            if (order.OrderStatus == Domain.Entities.Enums.OrderStatus.IptalEdildi)
                return new ResponseModel<NoContent>("Sipariş zaten iptal edilmiş.", 400);

            order.OrderStatus = Domain.Entities.Enums.OrderStatus.IptalEdildi;
            order.UpdatedDate = DateTime.UtcNow;

         
            foreach (var item in order.OrderItems)
            {
                await _reservationService.ReleaseStockAsync(item.ProductAttributeValueId.Value, item.Quantity);
            }

            await _orderWriteRepository.UpdateAsync(order);
            await _orderWriteRepository.SaveChangesAsync();

            return new ResponseModel<NoContent>(new NoContent(),204);
        }
    }
}
