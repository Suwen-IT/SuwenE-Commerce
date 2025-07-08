using Application.Common.Models;
using MediatR;

namespace Application.Features.Products.Commands;

public class DeleteProductCommandRequest:IRequest<ResponseModel<int>>
{
    public int Id { get; set; }

    public DeleteProductCommandRequest(int id)
    {
        Id = id;
    }
        
}