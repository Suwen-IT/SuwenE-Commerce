using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem:BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
        public int? ProductAttributeValueId { get; set; }
        public ProductAttributeValue? ProductAttributeValue { get; set; } 
    }
}
