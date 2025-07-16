using Application.Features.DTOs.Orders;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class OrderMap:Profile
    {
        public OrderMap()
        {
            // Entity -> DTO
            CreateMap<Order, OrderDto>()
;
        }

    }
}
