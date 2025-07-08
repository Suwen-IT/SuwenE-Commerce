using Application.Common.Models;
using Application.Features.Products.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Handlers;

public class CreateProductCommandHandler:IRequestHandler<CreateProductCommandRequest,ResponseModel<int>>
{
    private readonly IWriteRepository<Product> _repository;

    public CreateProductCommandHandler(IWriteRepository<Product> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseModel<int>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId,

        };
        var addResult = await _repository.AddAsync(product);
        
        if(!addResult)
            return new ResponseModel<int>("Product could not be created",404);

        await _repository.SaveChanges();
        return new ResponseModel<int>("Product created successfully",200);

    }
}