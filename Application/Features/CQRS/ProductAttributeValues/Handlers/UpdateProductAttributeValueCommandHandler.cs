using Application.Common.Models;
using Application.Features.CQRS.ProductAttributeValues.Commands;
using Application.Features.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

public class UpdateProductAttributeValueCommandHandler : IRequestHandler<UpdateProductAttributeValueCommandRequest, ResponseModel<ProductAttributeValueDto>>
{
    private readonly IReadRepository<ProductAttributeValue> _readRepository;
    private readonly IWriteRepository<ProductAttributeValue> _writeRepository;
    private readonly IReadRepository<Product> _productReadRepository;
    private readonly IReadRepository<ProductAttribute> _productAttributeReadRepository;
    private readonly IMapper _mapper;

    public UpdateProductAttributeValueCommandHandler(
        IReadRepository<ProductAttributeValue> readRepository,
        IWriteRepository<ProductAttributeValue> writeRepository,
        IReadRepository<Product> productReadRepository,
        IReadRepository<ProductAttribute> productAttributeReadRepository,
        IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _productReadRepository = productReadRepository;
        _productAttributeReadRepository = productAttributeReadRepository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductAttributeValueDto>> Handle(UpdateProductAttributeValueCommandRequest request, CancellationToken cancellationToken)
    {
        var existingEntity = await _readRepository.GetByIdAsync(request.Id);
        if (existingEntity == null)
            return new ResponseModel<ProductAttributeValueDto>("Güncellenmek istenen ürün niteliği değeri bulunamadı.", 404);

        var productExists = await _productReadRepository.GetByIdAsync(request.ProductId);
        if (productExists == null)
            return new ResponseModel<ProductAttributeValueDto>("Ürün bulunamadı.", 404);

        var attributeExists = await _productAttributeReadRepository.GetByIdAsync(request.ProductAttributeId);
        if (attributeExists == null)
            return new ResponseModel<ProductAttributeValueDto>("Ürün niteliği bulunamadı.", 404);

        _mapper.Map(request, existingEntity);

        await _writeRepository.UpdateAsync(existingEntity);
        var saved = await _writeRepository.SaveChangesAsync();

        if (!saved)
            return new ResponseModel<ProductAttributeValueDto>("Ürün niteliği değeri güncellenirken bir hata oluştu.", 500);

        var dto = _mapper.Map<ProductAttributeValueDto>(existingEntity);
        return new ResponseModel<ProductAttributeValueDto>(dto, 200);
    }
}
