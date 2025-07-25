using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using AutoMapper;
using Domain.Entities.Products;


namespace Application.Mappers
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductAttributeValue, ProductAttributeValueDto>()
                .ForMember(dest => dest.ProductAttributeName, opt => opt.MapFrom(src => src.ProductAttribute.Name));

            CreateMap<CreateProductCommandRequest, Product>()
                .ForMember(dest => dest.ProductAttributeValues, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.BasketItems, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());


            CreateMap<UpdateProductCommandRequest,Product>()
                .ForMember(dest=>dest.Id , opt => opt.Ignore())
                .ForMember(dest=>dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ProductAttributeValues, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.BasketItems, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());
        }
    }
}
