using Application.Common.Models;
using Application.Features.DTOs.Products;
using MediatR;

namespace Application.Features.CQRS.Products.Queries;

public class GetAllProductsQueryRequest:IRequest<ResponseModel<List<ProductDto>>>
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;

}