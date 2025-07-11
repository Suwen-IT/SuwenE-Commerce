using Application.Common.Models;
using Application.Features.DTOs.Products;
using MediatR;

namespace Application.Features.CQRS.Products.Queries;

public class GetProductByIdQueryRequest:IRequest<ResponseModel<ProductDto>>
{
    public int Id { get; set; }

}