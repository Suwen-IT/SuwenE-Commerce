using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CQRS.Products.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, ResponseModel<NoContent>>
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
       
    public async Task<ResponseModel<NoContent>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetAsync(
            predicate: p => p.Id == request.Id,
            include: query => query.Include(p => p.Category));

        if (product == null)
            return new ResponseModel<NoContent>("Ürün bulunamadý veya zaten silinmiþ", 204);

        await _writeRepository.DeleteAsync(product);
        var saved = await _writeRepository.SaveChangesAsync();

        if (!saved)
            return new ResponseModel<NoContent>("Ürün silinirken bir sorun oluþtu", 500);

        var productDto = _mapper.Map<NoContent>(product);
        return new ResponseModel<NoContent>(productDto, 200);

    }
}