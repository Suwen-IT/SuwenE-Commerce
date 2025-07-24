using Application.Features.CQRS.ProductAttributeValues.Commands;
using Application.Features.DTOs.Products;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ProductAttributeValueProfile:Profile
    {
        public ProductAttributeValueProfile()
        {
            
            CreateMap<ProductAttributeValue, ProductAttributeValueDto>()
                .ForMember(dest => dest.ProductAttributeName, opt => opt.MapFrom(src => src.ProductAttribute.Name))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));


            CreateMap<CreateProductAttributeValueCommandRequest, ProductAttributeValue>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.ProductAttribute, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

            CreateMap<UpdateProductAttributeValueCommandRequest, ProductAttributeValue>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) 
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow)) 
            .ForMember(dest => dest.ProductAttribute, opt => opt.Ignore()) 
            .ForMember(dest => dest.Product, opt => opt.Ignore());
        }
    }
}
