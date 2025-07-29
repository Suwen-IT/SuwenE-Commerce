using Application.Common.Models;
using MediatR;

namespace Application.Features.CQRS.Products.Commands;

public class DeleteProductCommandRequest:IRequest<ResponseModel<NoContent>>
{
    public int Id { get; set; }
        
}