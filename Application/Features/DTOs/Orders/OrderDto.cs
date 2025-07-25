using Application.Features.DTOs.Addresses;
using Domain.Entities.Enums;

namespace Application.Features.DTOs.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public Guid AppUserId { get; set; }
        public string AppUserName { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public int ShippingAddressId { get; set; }
        public AddressDto ShippingAddress { get; set; } = default!; 

        public int? BillingAddressId { get; set; }
        public AddressDto? BillingAddress { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
