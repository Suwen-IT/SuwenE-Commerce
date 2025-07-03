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
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            // DTO -> Entity (Sipariş oluştururken)
            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<OrderDetailCreateDto, OrderDetail>();
        }

    }
}
