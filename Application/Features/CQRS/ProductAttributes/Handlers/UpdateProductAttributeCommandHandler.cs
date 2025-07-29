using Application.Common.Models;
using Application.Features.CQRS.ProductAttributes.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Handlers
{
    public class UpdateProductAttributeCommandHandler : IRequestHandler<UpdateProductAttributeCommandRequest, ResponseModel<ProductAttributeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductAttributeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ProductAttributeDto>> Handle(UpdateProductAttributeCommandRequest request, CancellationToken cancellationToken)
        {
            var existingEntity = await _unitOfWork.GetReadRepository<ProductAttribute>().GetByIdAsync(request.Id);
            if (existingEntity == null)
            {
                return new ResponseModel<ProductAttributeDto>("Güncellemek istenen ürün niteliği bulunmadı", 404);
            }

            _mapper.Map(request, existingEntity);

            await _unitOfWork.GetWriteRepository<ProductAttribute>().UpdateAsync(existingEntity);
            var saved = await _unitOfWork.SaveChangesBoolAsync();

            if (!saved)
            {
                return new ResponseModel<ProductAttributeDto>("Ürün niteliği güncellenirken bir hata oluştu", 500);
            }

            var dto = _mapper.Map<ProductAttributeDto>(existingEntity);
            return new ResponseModel<ProductAttributeDto>(dto, 200);
        }
    }
}