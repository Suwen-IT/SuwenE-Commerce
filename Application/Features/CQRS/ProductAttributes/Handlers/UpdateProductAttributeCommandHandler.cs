using Application.Common.Models;
using Application.Features.CQRS.ProductAttributes.Commands;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Features.CQRS.ProductAttributes.Handlers
{
    public class UpdateProductAttributeCommandHandler : IRequestHandler<UpdateProductAttributeCommandRequest, ResponseModel<ProductAttributeDto>>
    {
        private readonly IReadRepository<ProductAttribute> _readRepository;
        private readonly IWriteRepository<ProductAttribute> _writeRepository;
        private IMapper _mapper;

        public UpdateProductAttributeCommandHandler(IReadRepository<ProductAttribute> readRepository, 
            IWriteRepository<ProductAttribute> writeRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ProductAttributeDto>> Handle(UpdateProductAttributeCommandRequest request, CancellationToken cancellationToken)
        {
            var existingEntity = await _readRepository.GetByIdAsync(request.Id);
            if(existingEntity == null)
            {
                return new ResponseModel<ProductAttributeDto>("Güncellemek istenen ürün niteliği bulunmadı", 404);
            }
            _mapper.Map(request,existingEntity);

            await _writeRepository.UpdateAsync(existingEntity);
            var saved = await _writeRepository.SaveChangesAsync();

            if (!saved)
            {
                return new ResponseModel<ProductAttributeDto>("Ürün niteliği güncellenirken bir hata oluştu", 500);
            }

            var dto=_mapper.Map<ProductAttributeDto>(existingEntity);
            return new ResponseModel<ProductAttributeDto>(dto, 200);
        }
    }
}
