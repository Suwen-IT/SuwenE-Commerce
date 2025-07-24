using Application.Common.Models;
using Application.Features.CQRS.ProductAttributeValues.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
public class CreateProductAttributeValueCommandHandler : IRequestHandler<CreateProductAttributeValueCommandRequest, ResponseModel<ProductAttributeValueDto>>
{
    private readonly IWriteRepository<ProductAttributeValue> _writeRepository;
    private readonly IReadRepository<Product> _productReadRepository;
    private readonly IReadRepository<ProductAttribute> _productAttributeReadRepository;
    private readonly IMapper _mapper;

    public CreateProductAttributeValueCommandHandler(
        IWriteRepository<ProductAttributeValue> writeRepository,
        IReadRepository<Product> productReadRepository,
        IReadRepository<ProductAttribute> productAttributeReadRepository,
        IMapper mapper)
    {
        _writeRepository = writeRepository;
        _productReadRepository = productReadRepository;
        _productAttributeReadRepository = productAttributeReadRepository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductAttributeValueDto>> Handle(CreateProductAttributeValueCommandRequest request, CancellationToken cancellationToken)
    {
        var productExists = await _productReadRepository.GetByIdAsync(request.ProductId);
        if (productExists == null)
            return new ResponseModel<ProductAttributeValueDto>("Ürün bulunamadı.", 404);

        var attributeExists = await _productAttributeReadRepository.GetByIdAsync(request.ProductAttributeId);
        if (attributeExists == null)
            return new ResponseModel<ProductAttributeValueDto>("Ürün niteliği bulunamadı.", 404);

        var entity = _mapper.Map<ProductAttributeValue>(request);

        await _writeRepository.AddAsync(entity);
        var saved = await _writeRepository.SaveChangesAsync();

        if (!saved)
            return new ResponseModel<ProductAttributeValueDto>("Ürün niteliği değeri oluşturulurken bir hata oluştu.", 500);

        var dto = _mapper.Map<ProductAttributeValueDto>(entity);
        return new ResponseModel<ProductAttributeValueDto>(dto, 201);
    }
}
