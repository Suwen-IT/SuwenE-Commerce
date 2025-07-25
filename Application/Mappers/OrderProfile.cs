using Application.Features.CQRS.Orders.Commands;
using Application.Features.DTOs.Orders;
using AutoMapper;
using Domain.Entities.Orders;

namespace Application.Mappers
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUser.UserName)) 
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress)) 
            .ForMember(dest => dest.BillingAddress, opt => opt.MapFrom(src => src.BillingAddress)) 
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)) 
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice)));
        

            
            CreateMap<CreateOrderCommandRequest, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingAddress, opt => opt.Ignore())
                .ForMember(dest => dest.BillingAddress, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.UtcNow)) 
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => Domain.Entities.Enums.OrderStatus.Beklemede)); 

           
            CreateMap<UpdateOrderCommandRequest, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.AppUser, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingAddress, opt => opt.Ignore())
                .ForMember(dest => dest.BillingAddress, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDate, opt => opt.Ignore()) 
                .ForMember(dest => dest.OrderStatus, opt => opt.Ignore()); 

          
            CreateMap<UpdateOrderStatusCommandRequest, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId)) 
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.NewStatus)) 
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow)) 
                .ForMember(dest => dest.AppUserId, opt => opt.Ignore()) 
                .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingAddressId, opt => opt.Ignore())
                .ForMember(dest => dest.BillingAddressId, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingAddress, opt => opt.Ignore())
                .ForMember(dest => dest.BillingAddress, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());
            ;
        }

    }
}
