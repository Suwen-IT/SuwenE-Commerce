using Application.Common.Models;
using Application.Features.DTOs.Products;
using Application.Interfaces.Validations;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Commands
{
    public class UpdateProductAttributeCommandRequest:IRequest<ResponseModel<ProductAttributeDto>>,IProductAttributeCommandBase
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
    }
}
