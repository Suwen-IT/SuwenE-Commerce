using Application.Common.Models;
using Application.Features.DTOs.Categories;
using MediatR;


namespace Application.Features.CQRS.Categories.Commands
{
    public class DeleteCategoryCommandRequest:IRequest<ResponseModel<NoContent>>
    {
        public int Id { get; set; }
        

    }
}
