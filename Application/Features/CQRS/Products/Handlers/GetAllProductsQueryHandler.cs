using Application.Common.Models;
using Application.Features.CQRS.Products.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, ResponseModel<List<ProductDto>>>
{
    private readonly IReadRepository<Product> _readRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IReadRepository<Product> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<List<ProductDto>>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _readRepository.GetAllAsyncByPaging(
            predicate: null,
            include: null,
            orderBy: null,
            enableTracking: false,
            currentPage: request.CurrentPage,
            pageSize: request.PageSize);

        var dtos = _mapper.Map<List<ProductDto>>(products);
        return new ResponseModel<List<ProductDto>>(dtos, 200);
    }
}