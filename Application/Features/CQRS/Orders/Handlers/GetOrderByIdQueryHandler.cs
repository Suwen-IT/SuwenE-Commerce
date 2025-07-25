using Application.Common.Models;
using Application.Features.CQRS.Orders.Queries;
using Application.Features.DTOs.Orders;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Orders.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, ResponseModel<OrderDto>>
    {
        private readonly IReadRepository<Order> _orderReadRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IReadRepository<Order> orderReadRepository, IMapper mapper)
        {
            _orderReadRepository = orderReadRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<OrderDto>> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderReadRepository.GetAsync(
            x => x.Id == request.Id && x.AppUserId == request.AppUserId,
            include: o => o
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.ProductAttributeValue)
                        .ThenInclude(pav => pav.ProductAttribute)
                .Include(x => x.ShippingAddress)
                .Include(x => x.BillingAddress)
            );

            if (order == null)
                return new ResponseModel<OrderDto>("Sipariş bulunamadı.", 404);

            var orderDto = _mapper.Map<OrderDto>(order);
            return new ResponseModel<OrderDto>(orderDto);
        }
    }
}
