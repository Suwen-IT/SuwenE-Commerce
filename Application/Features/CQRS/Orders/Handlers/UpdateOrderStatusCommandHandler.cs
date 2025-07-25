using Application.Common.Models;
using Application.Features.CQRS.Orders.Commands;
using Application.Features.DTOs.Orders;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.CQRS.Orders.Handlers
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommandRequest, ResponseModel<OrderDto>>
    {
        private readonly IReadRepository<Order> _orderReadRepository;
        private readonly IWriteRepository<Order> _orderWriteRepository;
        private readonly IMapper _mapper;

        public UpdateOrderStatusCommandHandler(
            IReadRepository<Order> orderReadRepository,
            IWriteRepository<Order> orderWriteRepository,
            IMapper mapper)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<OrderDto>> Handle(UpdateOrderStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderReadRepository.GetAsync(
            x => x.Id == request.OrderId && x.AppUserId == request.AppUserId);

            if (order == null)
                return new ResponseModel<OrderDto>("Sipariş bulunamadı.", 404);

            if (order.OrderStatus == request.NewStatus)
                return new ResponseModel<OrderDto>("Sipariş zaten bu statüde.", 400);

            order.OrderStatus = request.NewStatus;
            order.UpdatedDate= DateTime.Now;

            await _orderWriteRepository.UpdateAsync(order);
            var result = await _orderWriteRepository.SaveChangesAsync();

            if (!result)
                return new ResponseModel<OrderDto>("Statü güncellenmedi.", 500);

            var dto = _mapper.Map<OrderDto>(order);
            return new ResponseModel<OrderDto>(dto, 200);
        }
    }
}
