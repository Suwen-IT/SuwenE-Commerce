using Application.Common.Models;
using Application.Features.CQRS.ProductAttributeValues.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

public class UpdateProductAttributeValueCommandHandler : IRequestHandler<UpdateProductAttributeValueCommandRequest, ResponseModel<ProductAttributeValueDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductAttributeValueCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductAttributeValueDto>> Handle(UpdateProductAttributeValueCommandRequest request, CancellationToken cancellationToken)
    {
        var existingEntity = await _unitOfWork.GetReadRepository<ProductAttributeValue>().GetByIdAsync(request.Id);
        if (existingEntity == null)
            return new ResponseModel<ProductAttributeValueDto>("Güncellenmek istenen ürün niteliği değeri bulunamadı.", 404);

        var productExists = await _unitOfWork.GetReadRepository<Product>().GetByIdAsync(request.ProductId);
        if (productExists == null)
            return new ResponseModel<ProductAttributeValueDto>("Ürün bulunamadı.", 404);

        var attributeExists = await _unitOfWork.GetReadRepository<ProductAttribute>().GetByIdAsync(request.ProductAttributeId);
        if (attributeExists == null)
            return new ResponseModel<ProductAttributeValueDto>("Ürün niteliği bulunamadı.", 404);

        _mapper.Map(request, existingEntity);

        await _unitOfWork.GetWriteRepository<ProductAttributeValue>().UpdateAsync(existingEntity);
        var saved = await _unitOfWork.SaveChangesBoolAsync();

        if (!saved)
            return new ResponseModel<ProductAttributeValueDto>("Ürün niteliği değeri güncellenirken bir hata oluştu.", 500);

        var dto = _mapper.Map<ProductAttributeValueDto>(existingEntity);
        return new ResponseModel<ProductAttributeValueDto>(dto, 200);
    }
}
