using Application.Common.Models;
using Application.Features.CQRS.Orders.Commands;
using Application.Features.DTOs.Orders;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.CQRS.Orders.Handlers
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommandRequest, ResponseModel<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<OrderDto>> Handle(UpdateOrderStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.GetReadRepository<Order>()
                .GetAsync(x => x.Id == request.OrderId && x.AppUserId == request.AppUserId);

            if (order == null)
                return new ResponseModel<OrderDto>("Sipariş bulunamadı.", 404);

            if (order.OrderStatus == request.NewStatus)
                return new ResponseModel<OrderDto>("Sipariş zaten bu statüde.", 400);

            order.OrderStatus = request.NewStatus;
            order.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.GetWriteRepository<Order>().UpdateAsync(order);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                return new ResponseModel<OrderDto>("Statü güncellenmedi.", 500);

            var dto = _mapper.Map<OrderDto>(order);
            return new ResponseModel<OrderDto>(dto, 200);
        }
    }
}