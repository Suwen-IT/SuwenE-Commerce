using Application.Features.DTOs.Orders;
using AutoMapper;
using Domain.Entities.Orders;

namespace Application.Mappers
{
    public class OrderItemProfile:Profile
    {
        public OrderItemProfile()
        {
            
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name)) 
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl)) 
                .ForMember(dest => dest.ProductAttributeValueName, opt => opt.MapFrom(src =>
                    src.ProductAttributeValue != null ? src.ProductAttributeValue.Value : null))
                .ForMember(dest => dest.ProductAttributeName, opt => opt.MapFrom(src =>
                    src.ProductAttributeValue != null ? src.ProductAttributeValue.ProductAttribute.Name : null)); 
        }
    }
}
