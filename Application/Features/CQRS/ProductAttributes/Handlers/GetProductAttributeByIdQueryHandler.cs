using Application.Common.Models;
using Application.Features.CQRS.ProductAttributes.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Handlers
{
    public class GetProductAttributeByIdQueryHandler : IRequestHandler<GetProductAttributeByIdQueryRequest, ResponseModel<ProductAttributeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductAttributeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ProductAttributeDto>> Handle(GetProductAttributeByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GetReadRepository<ProductAttribute>().GetByIdAsync(request.Id);

            if (entity == null)
                return new ResponseModel<ProductAttributeDto>("Belirtilen ürün niteliği bulunamadı.", 404);

            var dto = _mapper.Map<ProductAttributeDto>(entity);
            return new ResponseModel<ProductAttributeDto>(dto, 200);
        }
    }
}