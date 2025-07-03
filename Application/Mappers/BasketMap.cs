using Application.Features.DTOs.Baskets;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class BasketMap:Profile
    {
        public BasketMap()
        {
            CreateMap<Basket, BasketDto>();
            CreateMap<BasketCreateDto, Basket>();

            CreateMap<BasketItem, BasketItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<BasketItemCreateDto, BasketItem>();
        }
    }
}
