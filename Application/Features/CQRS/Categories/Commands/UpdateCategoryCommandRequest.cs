using Application.Common.Models;
using Application.Features.DTOs.Categories;
using Application.Interfaces.Validations;
using MediatR;


namespace Application.Features.CQRS.Categories.Commands
{
    public class UpdateCategoryCommandRequest:IRequest<ResponseModel<CategoryDto>>, ICategoryCommandBase
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
    }
}
