using Application.Common.Models;
using Application.Features.DTOs.Products;
using MediatR;

namespace Application.Features.CQRS.ProductAttributeValues.Queries
{
    public class GetProductAttributeValueByIdQueryRequest : IRequest<ResponseModel<ProductAttributeValueDto>>
    {
        public int Id { get; set; }
    }
}
