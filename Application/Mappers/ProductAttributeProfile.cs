using Application.Features.CQRS.ProductAttributes.Commands;
using Application.Features.DTOs;
using AutoMapper;
using System;
using Domain.Entities;

namespace Application.Mappers
{
    public class ProductAttributeProfile: Profile
    {
        public ProductAttributeProfile()
        {
            CreateMap<ProductAttribute, ProductAttributeDto>();

            CreateMap<CreateProductAttributeCommandRequest, ProductAttribute>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest=>dest.ProductAttributeValues,opt=>opt.Ignore());

            CreateMap<UpdateProductAttributeCommandRequest, ProductAttribute>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(_ =>DateTime.UtcNow))
                .ForMember(dest => dest.ProductAttributeValues, opt => opt.Ignore());
        }
    }
}
