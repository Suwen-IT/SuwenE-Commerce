using Application.Common.Models;
using Application.Features.DTOs.Products;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Queries
{
    public class GetProductAttributeByIdQueryRequest:IRequest<ResponseModel<ProductAttributeDto>>
    {
        public int Id { get; set; }

        public GetProductAttributeByIdQueryRequest(int id)
        {
            Id = id;
        }
    }
}
