using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Products;
using MediatR;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, ResponseModel<ProductDto>>
{
    private readonly IReadRepository<Product> _readRepository;
    private readonly IReadRepository<Category> _categoryReadRepository;
    private readonly IWriteRepository<Product> _writeRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(
        IReadRepository<Product> readRepository,
        IReadRepository<Category> categoryReadRepository,
        IWriteRepository<Product> writeRepository,
        IMapper mapper)
    {
        _readRepository = readRepository;
        _categoryReadRepository = categoryReadRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductDto>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var existingEntity = await _readRepository.GetByIdAsync(request.Id);
        if (existingEntity == null)
            return new ResponseModel<ProductDto>("Güncellenmek istenen ürün bulunamadı.", 404);

        var categoryExists = await _categoryReadRepository.GetByIdAsync(request.CategoryId);
        if (categoryExists == null)
            return new ResponseModel<ProductDto>("Kategori bulunamadı.", 404);

        _mapper.Map(request, existingEntity);

        await _writeRepository.UpdateAsync(existingEntity);
        var saved = await _writeRepository.SaveChangesAsync();

        if (!saved)
            return new ResponseModel<ProductDto>("Ürün güncellenirken bir hata oluştu.", 500);

        var dto = _mapper.Map<ProductDto>(existingEntity);
        return new ResponseModel<ProductDto>(dto, 200);
    }
}
