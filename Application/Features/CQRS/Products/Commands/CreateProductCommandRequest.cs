using Application.Common.Models;
using Application.Features.DTOs.Products;
using Application.Interfaces.Validations;
using MediatR;

namespace Application.Features.CQRS.Products.Commands;

public class CreateProductCommandRequest:IRequest<ResponseModel<ProductDto>>, IProductCommandBase
{
    public string Name { get; set; }= string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    
}