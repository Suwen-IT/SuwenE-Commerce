using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs.Orders
{
    public class OrderCreateDto
    {
        public decimal TotalPrice { get; set; }
        public List<OrderDetailCreateDto> OrderDetails { get; set; }
    }
}
