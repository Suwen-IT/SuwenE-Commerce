using Application.Common.Models;
using Application.Features.DTOs.Categories;
using MediatR;


namespace Application.Features.CQRS.Categories.Queries
{
    public class GetCategoryByIdQueryRequest:IRequest<ResponseModel<CategoryDto>>
    {
        public int Id { get; set; }

    }
}
