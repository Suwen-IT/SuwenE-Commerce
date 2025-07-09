using Application.Common.Models;
using Application.Features.Products.Commands;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.Features.Products.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, ResponseModel<int>>
{
    private readonly IWriteRepository<Product> _writeRepository;
    private readonly IReadRepository<Product> _readRepository;
    private readonly IMapper _mapper;
    public UpdateProductCommandHandler(IWriteRepository<Product> writeRepository, IReadRepository<Product> readRepository, IMapper mapper)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _mapper = mapper;
    }
    public async Task<ResponseModel<int>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetByIdAsync(request.Id);
        if (product == null)
            return new ResponseModel<int>("Product not found", 404);
        _mapper.Map(request, product);

        await _writeRepository.UpdateAsync(product);
        await _writeRepository.SaveChangesAsync();

        return new ResponseModel<int>(product.Id, 200);
    }
}