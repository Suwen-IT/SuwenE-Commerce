using Application.Common.Models;
using Application.Features.DTOs.Products;
using MediatR;

namespace Application.Features.CQRS.Products.Commands;

public class DeleteProductCommandRequest:IRequest<ResponseModel<ProductDto>>
{
    public int Id { get; set; }
        
}