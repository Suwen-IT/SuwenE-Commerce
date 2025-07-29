using Application.Common.Models;
using Application.Features.CQRS.ProductAttributes.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.CQRS.ProductAttributes.Handlers
{
    public class GetAllProductAttributesQueryHandler : IRequestHandler<GetAllProductAttributesQueryRequest, ResponseModel<List<ProductAttributeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductAttributesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ProductAttributeDto>>> Handle(GetAllProductAttributesQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.GetReadRepository<ProductAttribute>().GetAllAsync();

            if (!list.Any())
                return new ResponseModel<List<ProductAttributeDto>>("Ürün niteliği bulunmamaktadır.", 404);

            var dtoList = _mapper.Map<List<ProductAttributeDto>>(list);
            return new ResponseModel<List<ProductAttributeDto>>(dtoList, 200);
        }
    }
}