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
    public class GetAllOrdersByUserIdQueryHandler : IRequestHandler<GetAllOrdersByUserIdQueryRequest, ResponseModel<List<OrderDto>>>
    {
        private readonly IReadRepository<Order> _orderReadRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersByUserIdQueryHandler(IReadRepository<Order> orderReadRepository, IMapper mapper)
        {
            _orderReadRepository = orderReadRepository;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<OrderDto>>> Handle(GetAllOrdersByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy=null;

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                if (request.SortBy.ToLower() == "orderdate")
                {
                    orderBy=q=>request.SortOrder?.ToLower()=="asc"
                    ? q.OrderBy(o=>o.OrderDate)
                    : q.OrderByDescending(o=>o.OrderDate);
                }
            }

            var orders = await _orderReadRepository.GetAllAsyncByPaging(
                predicate: x => x.AppUserId == request.AppUserId,
                include: o => o
                    .Include(x => x.OrderItems)
                      .ThenInclude(x => x.Product)
                    .Include(x => x.OrderItems)
                      .ThenInclude(x => x.ProductAttributeValue)
                        .ThenInclude(pav => pav.ProductAttribute)
                    .Include(x => x.ShippingAddress)
                    .Include(x => x.BillingAddress),

                orderBy: orderBy,
                currentPage: request.CurrentPage,
                pageSize: request.PageSize,
                enableTracking: false
                );

            if (orderBy == null || !orders.Any())
                return new ResponseModel<List<OrderDto>>("Sipariş bulunamadı", 404);

            var mappedOrders=_mapper.Map<List<OrderDto>>(orders);
            return new ResponseModel<List<OrderDto>>(mappedOrders, 200);
        }
    }
}
