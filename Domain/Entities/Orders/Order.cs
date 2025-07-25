using Domain.Entities.Base;
using Domain.Entities.Identity;
using Domain.Entities.Enums;

namespace Domain.Entities.Orders
{
    public class Order: BaseEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = default!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Beklemede;
        
        public int ShippingAddressId { get; set; }
        public Address ShippingAddress { get; set; } = default!;
        
        public int? BillingAddressId { get; set; }
        public Address? BillingAddress { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }=new HashSet<OrderItem>();
    }
}
