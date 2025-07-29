using Application.Common.Models;
using Application.Features.CQRS.Products.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Products;
using MediatR;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, ResponseModel<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<ProductDto>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var existingEntity = await _unitOfWork.GetReadRepository<Product>().GetByIdAsync(request.Id);
        if (existingEntity == null)
            return new ResponseModel<ProductDto>("Güncellenmek istenen ürün bulunamadı.", 404);

        var categoryExists = await _unitOfWork.GetReadRepository<Category>().GetByIdAsync(request.CategoryId);
        if (categoryExists == null)
            return new ResponseModel<ProductDto>("Kategori bulunamadı.", 404);

        _mapper.Map(request, existingEntity);

        await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(existingEntity);
        var saved = await _unitOfWork.SaveChangesBoolAsync();

        if (!saved)
            return new ResponseModel<ProductDto>("Ürün güncellenirken bir hata oluştu.", 500);

        var dto = _mapper.Map<ProductDto>(existingEntity);
        return new ResponseModel<ProductDto>(dto, 200);
    }
}