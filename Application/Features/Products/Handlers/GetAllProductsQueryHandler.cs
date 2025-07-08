using Application.Common.Models;
using Application.Features.DTOs.Products;
using Application.Features.Products.Queries;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Handlers;

public class GetAllProductsQueryHandler:IRequestHandler<GetAllProductsQueryRequest,ResponseModel<List<ProductDto>>>
{
    private readonly IReadRepository<Product> _repository;

    public GetAllProductsQueryHandler(IReadRepository<Product> repository)
    {
        _repository = repository;
    }
    public async Task<ResponseModel<List<ProductDto>>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync();
        
        if(products is null)
            return new ResponseModel<List<ProductDto>>("No products found",404);

        var productDtos = products.Select(product => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryName = product.Category.Name,
            CreatedTime = product.CreatedTime,

        }).ToList();
        return new ResponseModel<List<ProductDto>>(productDtos, 200);
    }
}