using Application.Common.Models;
using Application.Features.DTOs.Products;
using Application.Interfaces.Validations;
using MediatR;

namespace Application.Features.CQRS.Products.Commands;

public class UpdateProductCommandRequest:IRequest<ResponseModel<ProductDto>>,IProductCommandBase
{
   public int Id { get; set; }
   public string Name { get; set; }=string.Empty;
   public string Description { get; set; }= string.Empty;
   public decimal Price { get; set; }
   public string ImageUrl { get; set; } = string.Empty;
   public int CategoryId { get; set; }
   
}