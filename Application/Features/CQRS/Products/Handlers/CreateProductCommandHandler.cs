using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Products;
using MediatR;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, ResponseModel<ProductDto>>
{
    private readonly IWriteRepository<Product> _writeRepository;
    private readonly IReadRepository<Category> _categoryReadRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(
        IWriteRepository<Product> writeRepository, 
        IReadRepository<Category> categoryReadRepository,
        IMapper mapper)
    {
        _writeRepository = writeRepository;
        _categoryReadRepository = categoryReadRepository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductDto>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var categoryExists = await _categoryReadRepository.GetByIdAsync(request.CategoryId);
        if (categoryExists == null)
            return new ResponseModel<ProductDto>("Kategori bulunamadı.", 404);
        
        var entity = _mapper.Map<Product>(request);

        await _writeRepository.AddAsync(entity);

        var saved = await _writeRepository.SaveChangesAsync();

        if (!saved)
            return new ResponseModel<ProductDto>("Ürün oluşturulurken bir hata oluştu.", 500);

        var dto = _mapper.Map<ProductDto>(entity);
        return new ResponseModel<ProductDto>(dto, 201);
    }
}