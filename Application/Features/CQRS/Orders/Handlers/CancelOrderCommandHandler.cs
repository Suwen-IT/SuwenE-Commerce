using Application.Common.Models;
using Application.Features.CQRS.Orders.Commands;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Orders.Handlers
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommandRequest, ResponseModel<NoContent>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationService _reservationService;

        public CancelOrderCommandHandler(IUnitOfWork unitOfWork, IReservationService reservationService)
        {
            _unitOfWork = unitOfWork;
            _reservationService = reservationService;
        }

        public async Task<ResponseModel<NoContent>> Handle(CancelOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.GetReadRepository<Order>().GetAsync(
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
                if (item.ProductAttributeValueId.HasValue)
                {
                    await _reservationService.ReleaseStockAsync(item.ProductAttributeValueId.Value, item.Quantity);
                }
            }

            await _unitOfWork.GetWriteRepository<Order>().UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<NoContent>(new NoContent(), 204);
        }
    }
}
