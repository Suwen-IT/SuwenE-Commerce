using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs.Baskets
{
    public class BasketDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } 
    }
}
