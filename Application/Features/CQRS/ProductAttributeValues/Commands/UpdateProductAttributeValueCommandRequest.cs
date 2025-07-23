using Application.Common.Models;
using Application.Features.DTOs;
using Application.Interfaces.Validations;
using MediatR;


namespace Application.Features.CQRS.ProductAttributeValues.Commands
{
    public class UpdateProductAttributeValueCommandRequest : IRequest<ResponseModel<ProductAttributeValueDto>>,
        IProductAttributeValueCommandBase
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public int Stock { get; set; }
        public int ReservedStock { get; set; }

    }
}
