using Application.Common.Models;
using Application.Features.CQRS.Orders.Commands;
using Application.Features.DTOs.Orders;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.CQRS.Orders.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest, ResponseModel<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<OrderDto>> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.GetReadRepository<Order>().GetAsync(x => x.Id == request.OrderId && x.AppUserId == request.AppUserId);

            if (order == null)
                return new ResponseModel<OrderDto>("Sipariş bulunamadı", 404);

            var shippingAddress = await _unitOfWork.GetReadRepository<Domain.Entities.Address>().GetByIdAsync(request.ShippingAddressId);
            if (shippingAddress == null)
                return new ResponseModel<OrderDto>("Teslimat adresi bulunamadı", 404);
            order.ShippingAddressId = request.ShippingAddressId;

            if (request.BillingAddressId.HasValue)
            {
                var billingAddress = await _unitOfWork.GetReadRepository<Domain.Entities.Address>().GetByIdAsync(request.BillingAddressId.Value);
                if (billingAddress == null)
                    return new ResponseModel<OrderDto>("Fatura adresi bulunamadı", 404);
                order.BillingAddressId = request.BillingAddressId.Value;
            }

            order.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.GetWriteRepository<Order>().UpdateAsync(order);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                return new ResponseModel<OrderDto>("Sipariş güncellenemedi", 500);

            var dto = _mapper.Map<OrderDto>(order);
            return new ResponseModel<OrderDto>(dto, 200);
        }
    }
}
