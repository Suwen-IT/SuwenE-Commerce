using Application.Common.Models;
using Application.Features.CQRS.ProductAttributeValues.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

public class GetAllProductAttributeValuesQueryHandler : IRequestHandler<GetAllProductAttributeValuesQueryRequest, ResponseModel<List<ProductAttributeValueDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductAttributeValuesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<List<ProductAttributeValueDto>>> Handle(GetAllProductAttributeValuesQueryRequest request, CancellationToken cancellationToken)
    {
        var list = await _unitOfWork.GetReadRepository<ProductAttributeValue>().GetAllAsync();

        var dtos = _mapper.Map<List<ProductAttributeValueDto>>(list);
        return new ResponseModel<List<ProductAttributeValueDto>>(dtos, 200);
    }
}