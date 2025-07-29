using Application.Common.Models;
using Application.Features.CQRS.ProductAttributes.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Handlers
{
    public class CreateProductAttributeCommandHandler : IRequestHandler<CreateProductAttributeCommandRequest, ResponseModel<ProductAttributeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductAttributeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ProductAttributeDto>> Handle(CreateProductAttributeCommandRequest request, CancellationToken cancellationToken)
        {
            var productAttribute = _mapper.Map<ProductAttribute>(request);

            await _unitOfWork.GetWriteRepository<ProductAttribute>().AddAsync(productAttribute);
            var saved = await _unitOfWork.SaveChangesBoolAsync();

            if (!saved)
                return new ResponseModel<ProductAttributeDto>("Ürün niteliği oluşturulurken bir sorun oluştu", 500);

            var dto = _mapper.Map<ProductAttributeDto>(productAttribute);
            return new ResponseModel<ProductAttributeDto>(dto, 201);
        }
    }
}