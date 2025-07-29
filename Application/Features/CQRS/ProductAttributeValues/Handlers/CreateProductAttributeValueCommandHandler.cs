using Application.Common.Models;
using Application.Features.CQRS.ProductAttributeValues.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

public class CreateProductAttributeValueCommandHandler : IRequestHandler<CreateProductAttributeValueCommandRequest, ResponseModel<ProductAttributeValueDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductAttributeValueCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductAttributeValueDto>> Handle(CreateProductAttributeValueCommandRequest request, CancellationToken cancellationToken)
    {
        var productExists = await _unitOfWork.GetReadRepository<Product>().GetByIdAsync(request.ProductId);
        if (productExists == null)
            return new ResponseModel<ProductAttributeValueDto>("Ürün bulunamadı.", 404);

        var attributeExists = await _unitOfWork.GetReadRepository<ProductAttribute>().GetByIdAsync(request.ProductAttributeId);
        if (attributeExists == null)
            return new ResponseModel<ProductAttributeValueDto>("Ürün niteliği bulunamadı.", 404);

        var entity = _mapper.Map<ProductAttributeValue>(request);

        await _unitOfWork.GetWriteRepository<ProductAttributeValue>().AddAsync(entity);
        var saved = await _unitOfWork.SaveChangesBoolAsync();

        if (!saved)
            return new ResponseModel<ProductAttributeValueDto>("Ürün niteliği değeri oluşturulurken bir hata oluştu.", 500);

        var dto = _mapper.Map<ProductAttributeValueDto>(entity);
        return new ResponseModel<ProductAttributeValueDto>(dto, 201);
    }
}