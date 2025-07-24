using Application.Common.Models;
using Application.Features.CQRS.ProductAttributes.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Handlers
{
    public class CreateProductAttributeCommandHandler : IRequestHandler<CreateProductAttributeCommandRequest, ResponseModel<ProductAttributeDto>>
    {
        private readonly IWriteRepository<ProductAttribute> _writeRepository;
        private readonly IMapper _mapper;

        public CreateProductAttributeCommandHandler(IWriteRepository<ProductAttribute> writeRepository, IMapper mapper)
        {
           
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ProductAttributeDto>> Handle(CreateProductAttributeCommandRequest request, CancellationToken cancellationToken)
        {
            var productAttribute=_mapper.Map<ProductAttribute>(request);

            await _writeRepository.AddAsync(productAttribute);
            var saved = await _writeRepository.SaveChangesAsync();

            if (!saved)
            {
                return new ResponseModel<ProductAttributeDto> ("Ürün niteliği oluşturulurken bir sorun oluştu",500);
            }
            var dto = _mapper.Map<ProductAttributeDto>(productAttribute);
            return new ResponseModel<ProductAttributeDto>(dto,200);
            
        }
    }
}
