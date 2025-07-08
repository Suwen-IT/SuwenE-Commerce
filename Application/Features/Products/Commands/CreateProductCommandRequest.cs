using Application.Common.Models;
using MediatR;

namespace Application.Features.Products.Commands;

public class CreateProductCommandRequest:IRequest<ResponseModel<int>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    
}