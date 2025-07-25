using Application.Common.Models;
using Application.Features.CQRS.Orders.Commands;
using Application.Features.DTOs.Orders;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.CQRS.Orders.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest, ResponseModel<OrderDto>>
    {
        private readonly IReadRepository<Order> _orderReadRepository;
        private readonly IWriteRepository<Order> _orderWriteRepository;
        private readonly IReadRepository<Address> _addressReadRepository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IReadRepository<Order> orderReadRepository, IWriteRepository<Order> orderWriteRepository,
        IReadRepository<Address> addressReadRepository,IMapper mapper)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _addressReadRepository = addressReadRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<OrderDto>> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderReadRepository.GetAsync(x => x.Id == request.OrderId && x.AppUserId == request.AppUserId);
            if (order == null)
                return new ResponseModel<OrderDto>("Sipariş bulunamadı", 404);

            var shippingAddress = await _addressReadRepository.GetByIdAsync(request.ShippingAddressId);
            if (shippingAddress == null)
                return new ResponseModel<OrderDto>("Teslimat adresi bulunamadı", 404);
            order.ShippingAddressId = request.ShippingAddressId;

            if (request.BillingAddressId.HasValue)
            {
                var billingAddress = await _addressReadRepository.GetByIdAsync(request.BillingAddressId.Value);
                if (billingAddress == null)
                    return new ResponseModel<OrderDto>("Fatura adresi bulunamadı", 404);
                order.BillingAddressId = request.BillingAddressId.Value;
            }

            order.UpdatedDate = DateTime.UtcNow;

            await _orderWriteRepository.UpdateAsync(order);
            var result = await _orderWriteRepository.SaveChangesAsync();

            if (!result)
                return new ResponseModel<OrderDto>("Sipariş güncellenemedi", 500);

            var dto = _mapper.Map<OrderDto>(order);
            return new ResponseModel<OrderDto>(dto, 200);
        }
    }
}
