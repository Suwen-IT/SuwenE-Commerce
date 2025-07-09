using Application.Features.DTOs.Products;
using Application.Features.Products.Commands;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class ProductMap:Profile
    {
        public ProductMap()
        {

            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ReverseMap();
            CreateMap<UpdateProductCommandRequest, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateProductCommandRequest, Product>();
            CreateMap<CreateProductCommandRequest, Product>();
        }
    }
}
