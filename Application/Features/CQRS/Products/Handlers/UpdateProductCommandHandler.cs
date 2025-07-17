using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Features.CQRS.Products.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, ResponseModel<ProductDto>>
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
    public async Task<ResponseModel<ProductDto>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            return new ResponseModel<ProductDto>( "Ürün Bulunamadý" , 404);
        }
        _mapper.Map(request, product);

        await _writeRepository.UpdateAsync(product);
       var saved= await _writeRepository.SaveChangesAsync();

        if (!saved)
        {
            return new ResponseModel<ProductDto>("Ürün güncellenmedi" , 500);
        }
        var updateDto = _mapper.Map<ProductDto>(product);
        return new ResponseModel<ProductDto>(updateDto, 200);
    }
}