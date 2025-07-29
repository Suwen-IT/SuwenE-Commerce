using Application.Common.Models;
using Application.Features.CQRS.Products.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, ResponseModel<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductDto>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.GetReadRepository<Product>().GetByIdAsync(request.Id);

        if (product == null)
            return new ResponseModel<ProductDto>("Ürün bulunamadı.", 404);

        var dto = _mapper.Map<ProductDto>(product);
        return new ResponseModel<ProductDto>(dto, 200);
    }
}