using Application.Features.CQRS.Categories.Commands;
using Application.Features.DTOs.Categories;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class CategoryMap:Profile
    {
        public CategoryMap()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<UpdateCategoryCommandRequest, Category>();

            CreateMap<CreateCategoyCommandRequest, Category>();
        }
    }
}
