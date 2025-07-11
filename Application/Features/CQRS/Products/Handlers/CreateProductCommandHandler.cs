using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Products.Handlers;

public class CreateProductCommandHandler:IRequestHandler<CreateProductCommandRequest,ResponseModel<int>>
{
    private readonly IWriteRepository<Product> _repository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IWriteRepository<Product> repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseModel<int>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
   
        var addResult = await _repository.AddAsync(product);
        
        if(!addResult)
            return new ResponseModel<int>("Product could not be created",400);

        await _repository.SaveChangesAsync();
        var productDto = _mapper.Map<ProductDto>(product);
        return new ResponseModel<int>(product.Id,200);

    }
}