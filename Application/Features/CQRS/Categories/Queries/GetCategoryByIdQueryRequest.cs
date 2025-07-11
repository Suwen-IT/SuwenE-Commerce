using Application.Common.Models;
using Application.Features.DTOs.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Categories.Queries
{
    public class GetCategoryByIdQueryRequest:IRequest<ResponseModel<CategoryDto>>
    {
        public int Id { get; set; }

       
    }
}
