using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Products.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, ResponseModel<ProductDto>>
{
    private readonly IReadRepository<Product> _readRepository;
    private readonly IWriteRepository<Product> _writeRepository;
    private readonly IMapper _mapper;

    public DeleteProductCommandHandler(IReadRepository<Product> readRepository, IWriteRepository<Product> writeRepository,IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }
       
    public async Task<ResponseModel<ProductDto>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetAsync(
            predicate: p => p.Id == request.Id,
            include: query => query.Include(p => p.Category));

        if (product == null)
            return new ResponseModel<ProductDto>("Ürün bulunamadý veya zaten silinmiþ", 404);

        await _writeRepository.DeleteAsync(product);
        var saved = await _writeRepository.SaveChangesAsync();

        if (!saved)
            return new ResponseModel<ProductDto>("Ürün silinirken bir sorun oluþtu", 500);

        var productDto = _mapper.Map<ProductDto>(product);
        return new ResponseModel<ProductDto>(productDto, 200);

    }
}