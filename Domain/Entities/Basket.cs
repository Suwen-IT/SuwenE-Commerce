using Domain.Base;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Basket:BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        
        public ICollection<BasketItem>BasketItems { get; set; }
    }
}
