using Domain.Entities.Base;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class Address: BaseEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string Title { get; set; }=string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; }=string.Empty;
        public string State { get; set; }=string.Empty;
        public string Street{ get; set; }=string.Empty;
        public string ZipCode { get; set; }=string.Empty;
        public bool IsDefault { get; set; }

        public ICollection<Order> ShippingOrders { get; set; } = new HashSet<Order>();
        public ICollection<Order> BillingOrders { get; set; }= new HashSet<Order>();
    }
}
