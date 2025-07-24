using Application.Common.Models;
using Application.Features.DTOs.Products;
using Application.Interfaces.Validations;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Commands
{
    public class CreateProductAttributeCommandRequest : IRequest<ResponseModel<ProductAttributeDto>>,IProductAttributeCommandBase
    {
        public string Name { get; set; }
    }
}
