using Application.Common.Models;
using Application.Features.CQRS.Products.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

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
        var product = await _repository.GetByIdWithCategoryAsync(request.Id);

        if (product == null)
            return new ResponseModel<ProductDto>("Product not found", 404);

        var productDto = _mapper.Map<ProductDto>(product);

        return new ResponseModel<ProductDto>(productDto, 200);
    }
}