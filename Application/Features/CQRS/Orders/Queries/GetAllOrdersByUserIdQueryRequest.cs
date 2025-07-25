using Application.Common.Models;
using Application.Features.DTOs.Orders;
using MediatR;

namespace Application.Features.CQRS.Orders.Queries
{
    public class GetAllOrdersByUserIdQueryRequest:IRequest<ResponseModel<List<OrderDto>>>
    {
        public Guid AppUserId { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set;} = 10;

        public string? SortBy { get; set; } = "OrderDate";
        public string? SortOrder { get; set; } = "desc";
    }
}
