using Application.Common.Models;
using Application.Features.DTOs.Products;
using Application.Features.Products.Queries;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Handlers;

public class GetProductsByIdQueryHandler:IRequestHandler<GetProductsByIdQueryRequest,ResponseModel<ProductDto>>
{
    private readonly IReadRepository<Product> _repository;

    public GetProductsByIdQueryHandler(IReadRepository<Product> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseModel<ProductDto>> Handle(GetProductsByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(p=>p.Id==request.Id);
        
        if(product == null)
            return new ResponseModel<ProductDto>("Product not found",404);

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            CreatedTime = product.CreatedTime,
        };
        return new ResponseModel<ProductDto>(productDto,200);
    }
}