using Application.Common.Models;
using Application.Features.CQRS.Orders.Queries;
using Application.Features.DTOs.Orders;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Orders.Handlers
{
    public class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQueryRequest, ResponseModel<List<OrderItemDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderItemByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<OrderItemDto>>> Handle(GetOrderItemByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var orderItems = await _unitOfWork.GetReadRepository<OrderItem>().GetAllAsync(
                predicate: oi => oi.OrderId == request.Id,
                include: oi => oi
                    .Include(x => x.Product)
                    .Include(x => x.ProductAttributeValue)
                    .ThenInclude(pav => pav.ProductAttribute),
                enableTracking: false
            );

            if (orderItems == null || !orderItems.Any())
            {
                return new ResponseModel<List<OrderItemDto>>("Siparişe ait ürün bulunamadı.", 404);
            }

            var mappedItems = _mapper.Map<List<OrderItemDto>>(orderItems);
            return new ResponseModel<List<OrderItemDto>>(mappedItems, 200);
        }
    }
}