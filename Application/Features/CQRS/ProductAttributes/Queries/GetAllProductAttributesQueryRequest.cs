using Application.Common.Models;
using Application.Features.DTOs.Products;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Queries
{
    public class GetAllProductAttributesQueryRequest:IRequest<ResponseModel<List<ProductAttributeDto>>>
    {
    }
}
