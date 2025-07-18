using Application.Common.Models;
using Application.Features.CQRS.Products.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Products.Handlers;

public class GetProductByIdQueryHandler:IRequestHandler<GetProductByIdQueryRequest,ResponseModel<ProductDto>>
{
    private readonly IReadRepository<Product> _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IReadRepository<Product> repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseModel<ProductDto>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(
            predicate: x => x.Id == request.Id,
            include: query => query.Include(p => p.Category)
                .Include(p => p.ProductAttributeValues)
                .ThenInclude(p => p.ProductAttribute));

        if (product == null)
        {
            return new ResponseModel<ProductDto>("Ürün bulunamadý.", 204);
        }
        var productDto = _mapper.Map<ProductDto>(product);
        return new ResponseModel<ProductDto>(productDto, 200);

    }
}