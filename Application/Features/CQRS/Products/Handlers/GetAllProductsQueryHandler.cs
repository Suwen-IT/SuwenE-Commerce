using Application.Common.Models;
using Application.Features.CQRS.Products.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, ResponseModel<List<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<List<ProductDto>>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsyncByPaging(
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