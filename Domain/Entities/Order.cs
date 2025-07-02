using Domain.Base;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order: BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
