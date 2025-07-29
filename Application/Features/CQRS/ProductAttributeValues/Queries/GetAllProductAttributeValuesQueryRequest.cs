using Application.Common.Models;
using Application.Features.DTOs.Products;
using MediatR;


namespace Application.Features.CQRS.ProductAttributeValues.Queries
{
    public class GetAllProductAttributeValuesQueryRequest : IRequest<ResponseModel<List<ProductAttributeValueDto>>>
    {
    }
}
