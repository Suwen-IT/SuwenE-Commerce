using Application.Features.DTOs.Baskets;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDto>()
           .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src =>
               src.BasketItems.Sum(item => item.UnitPrice * item.Quantity))) 
           .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src =>
               src.BasketItems.Count())) 
           .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUser.UserName)); 

            CreateMap<BasketItem, BasketItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl)) 
                .ForMember(dest => dest.ProductAttributeValueName, opt => opt.MapFrom(src =>
                    src.ProductAttributeValue != null ? src.ProductAttributeValue.Value : null)) 
                .ForMember(dest => dest.ProductAttributeName, opt => opt.MapFrom(src =>
                    src.ProductAttributeValue != null ? src.ProductAttributeValue.ProductAttribute.Name : null)) 
                .ForMember(dest => dest.ReservationExpirationDate, opt => opt.MapFrom(src => src.ReservationExpirationDate));
        }
    }
}
