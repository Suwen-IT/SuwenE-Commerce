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
    public class GetAllOrdersByUserIdQueryHandler : IRequestHandler<GetAllOrdersByUserIdQueryRequest, ResponseModel<List<OrderDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrdersByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<OrderDto>>> Handle(GetAllOrdersByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = null;

            if (!string.IsNullOrEmpty(request.SortBy) && request.SortBy.ToLower() == "orderdate")
            {
                orderBy = q => request.SortOrder?.ToLower() == "asc"
                    ? q.OrderBy(o => o.OrderDate)
                    : q.OrderByDescending(o => o.OrderDate);
            }

            var orders = await _unitOfWork.GetReadRepository<Order>().GetAllAsyncByPaging(
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
                enableTracking: false);

            if (orders == null || !orders.Any())
                return new ResponseModel<List<OrderDto>>("Sipariş bulunamadı", 404);

            var mappedOrders = _mapper.Map<List<OrderDto>>(orders);
            return new ResponseModel<List<OrderDto>>(mappedOrders, 200);
        }
    }
}
