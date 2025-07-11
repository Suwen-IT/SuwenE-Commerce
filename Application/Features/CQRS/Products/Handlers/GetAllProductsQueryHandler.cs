using Application.Common.Models;
using Application.Features.CQRS.Products.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using Application.Mappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Products.Handlers;

public class GetAllProductsQueryHandler:IRequestHandler<GetAllProductsQueryRequest,ResponseModel<List<ProductDto>>>
{
    private readonly IReadRepository<Product> _repository;
    private readonly IMapper _mapper;
    public GetAllProductsQueryHandler(IReadRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseModel<List<ProductDto>>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllWithCategoryAsync();

        if (products == null || !products.Any())
            return new ResponseModel<List<ProductDto>>("No products found", 404);

        var productDtos = _mapper.Map<List<ProductDto>>(products);

        return new ResponseModel<List<ProductDto>>(productDtos, 200);

    }
}