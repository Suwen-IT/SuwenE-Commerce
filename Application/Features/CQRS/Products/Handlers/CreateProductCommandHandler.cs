using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Products.Handlers;

public class CreateProductCommandHandler:IRequestHandler<CreateProductCommandRequest,ResponseModel<ProductDto>>
{
    private readonly IWriteRepository<Product> _writeRepository;
    private readonly IReadRepository<Category> _readRepository;
    private readonly IReadRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IWriteRepository<Product> writeRepository,IMapper mapper,
        IReadRepository<Category> readRepository,IReadRepository<Product>productRepository)
    {
        _writeRepository = writeRepository;
        _mapper = mapper;
        _readRepository = readRepository;
        _productRepository = productRepository;
    }
    
    public async Task<ResponseModel<ProductDto>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
       var category=await _readRepository.GetByIdAsync(request.CategoryId);
        if (category==null)
        {
            return new ResponseModel<ProductDto>( "Belirtilen kategori bulunmadý" , 400);
        }
        var product = _mapper.Map<Product>(request);

        await _writeRepository.AddAsync(product);
        var saved = await _writeRepository.SaveChangesAsync();
        if (!saved)
        {
            return new ResponseModel<ProductDto>( "Ürün eklenirken bir sorun oluþtu." , 500);
        }

        var createdProduct = await _productRepository.GetAsync(
            predicate: p => p.Id == product.Id,
            include: query => query.Include(p => p.Category));

        if (createdProduct == null)
        {
            return new ResponseModel<ProductDto>("Ürün baþarýyla oluturuldu fakat detay verisi alýnamadý." , 500);
        }

        var productDto = _mapper.Map<ProductDto>(createdProduct);
        return new ResponseModel<ProductDto>(productDto, 200);

    }
}