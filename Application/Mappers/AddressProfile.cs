using Application.Features.CQRS.Addresses.Commands;
using Application.Features.DTOs.Addresses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class AddressProfile:Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(
                    src => $"{src.Street}, {src.ZipCode} {src.City}/{src.State}, {src.Country}"));

            CreateMap<CreateAddressCommandRequest, Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore())
                .ForMember(dest => dest.ShippingOrders, opt => opt.Ignore())
                .ForMember(dest => dest.BillingOrders, opt => opt.Ignore());
            
            CreateMap<UpdateAddressCommandRequest,Address>()
                .ForMember(dest=>dest.CreatedDate,opt=>opt.Ignore())
                .ForMember(dest=>dest.UpdatedDate,opt=>opt.Ignore())
                .ForMember(dest=>dest.AppUser,opt=>opt.Ignore())
                .ForMember(dest=>dest.ShippingOrders,opt=>opt.Ignore())
                .ForMember(dest=>dest.BillingOrders,opt=>opt.Ignore());
            
        }

    }
}


