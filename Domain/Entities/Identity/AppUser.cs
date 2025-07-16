using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Identity
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; }=string.Empty;
        public override string? PhoneNumber { get; set; }=string.Empty;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Order> Orders { get; set; }=new HashSet<Order>();
        public ICollection<Address> Addresses { get; set; }=new HashSet<Address>();
        public ICollection<Basket> Baskets { get; set; }=new HashSet<Basket>();
        public ICollection<Review> Reviews { get; set; }=new HashSet<Review>();
        public ICollection<Notification> Notifications { get; set; }=new HashSet<Notification>();
 
    }
}
