using Application.Common.Models;
using Application.Features.CQRS.ProductAttributeValues.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

public class GetProductAttributeValueByIdQueryHandler : IRequestHandler<GetProductAttributeValueByIdQueryRequest, ResponseModel<ProductAttributeValueDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductAttributeValueByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductAttributeValueDto>> Handle(GetProductAttributeValueByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.GetReadRepository<ProductAttributeValue>().GetByIdAsync(request.Id);

        if (entity == null)
            return new ResponseModel<ProductAttributeValueDto>("Ürün niteliği değeri bulunamadı.", 404);

        var dto = _mapper.Map<ProductAttributeValueDto>(entity);
        return new ResponseModel<ProductAttributeValueDto>(dto, 200);
    }
}