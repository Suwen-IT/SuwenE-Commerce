using Application.Common.Models;
using Application.Features.CQRS.Products.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, ResponseModel<ProductDto>>
{
    private readonly IReadRepository<Product> _readRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IReadRepository<Product> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductDto>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetByIdAsync(request.Id);

        if (product == null)
            return new ResponseModel<ProductDto>("Ürün bulunamadı.", 404);

        var dto = _mapper.Map<ProductDto>(product);
        return new ResponseModel<ProductDto>(dto, 200);
    }
}