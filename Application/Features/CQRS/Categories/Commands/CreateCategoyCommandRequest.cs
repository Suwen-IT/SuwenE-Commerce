using Application.Common.Models;
using Application.Features.DTOs.Categories;
using Application.Interfaces.Validations;
using MediatR;


namespace Application.Features.CQRS.Categories.Commands
{
    public class CreateCategoyCommandRequest:IRequest<ResponseModel<CategoryDto>>,ICategoryCommandBase
    {
        public string Name { get; set; }
    }
    
}
