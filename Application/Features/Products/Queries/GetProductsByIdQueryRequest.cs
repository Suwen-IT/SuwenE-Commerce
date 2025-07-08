using Application.Common.Models;
using Application.Features.DTOs.Products;
using MediatR;

namespace Application.Features.Products.Queries;

public class GetProductsByIdQueryRequest:IRequest<ResponseModel<ProductDto>>
{
    public int Id { get; set; }

    public GetProductsByIdQueryRequest(int id)
    {
        Id = id;
    }
}