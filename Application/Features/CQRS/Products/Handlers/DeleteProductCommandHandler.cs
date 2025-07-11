using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Products.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, ResponseModel<int>>
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
       
    public async Task<ResponseModel<int>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetByIdAsync(request.Id);

        if (product == null)
            return new ResponseModel<int>("Product not found or already removed", 400);

        await _writeRepository.DeleteAsync(product);
        await _writeRepository.SaveChangesAsync();

        var productDto = _mapper.Map<ProductDto>(product);

        return new ResponseModel<int>(product.Id,200);
    }
}