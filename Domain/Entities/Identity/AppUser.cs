using Domain.Entities.Baskets;
using Domain.Entities.Orders;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; }=string.Empty;
        public override string? PhoneNumber { get; set; }=string.Empty;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }


        public ICollection<Order> Orders { get; set; }=new HashSet<Order>();
        public ICollection<Address> Addresses { get; set; }=new HashSet<Address>();
        public ICollection<Basket> Baskets { get; set; }=new HashSet<Basket>();
        public ICollection<Review> Reviews { get; set; }=new HashSet<Review>();
        public ICollection<Notification> Notifications { get; set; }=new HashSet<Notification>();
 
    }
}
