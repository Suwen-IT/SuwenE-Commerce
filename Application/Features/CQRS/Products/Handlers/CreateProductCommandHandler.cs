using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Products;
using MediatR;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, ResponseModel<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductDto>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var categoryExists = await _unitOfWork.GetReadRepository<Category>().GetByIdAsync(request.CategoryId);
        if (categoryExists == null)
            return new ResponseModel<ProductDto>("Kategori bulunamadı.", 404);

        var entity = _mapper.Map<Product>(request);
        
        await _unitOfWork.GetWriteRepository<Product>().AddAsync(entity);
        var saved = await _unitOfWork.SaveChangesBoolAsync();

        if (!saved)
            return new ResponseModel<ProductDto>("Ürün oluşturulurken bir hata oluştu.", 500);

        var dto = _mapper.Map<ProductDto>(entity);
        return new ResponseModel<ProductDto>(dto, 201);
    }
}