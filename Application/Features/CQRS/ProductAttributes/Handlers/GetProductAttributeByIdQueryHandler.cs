using Application.Common.Models;
using Application.Features.CQRS.ProductAttributes.Queries;
using Application.Features.DTOs.Products;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.ProductAttributes.Handlers
{
    public class GetProductAttributeByIdQueryHandler : IRequestHandler<GetProductAttributeByIdQueryRequest, ResponseModel<ProductAttributeDto>>
    {
        private readonly IReadRepository<ProductAttribute> _repository;
        private readonly IMapper _mapper;

        public GetProductAttributeByIdQueryHandler(IReadRepository<ProductAttribute> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ProductAttributeDto>> Handle(GetProductAttributeByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if(entity == null)
                return new ResponseModel<ProductAttributeDto>("Belirtilen nitelik bulunamadı", 404);
            
            var dto=_mapper.Map<ProductAttributeDto>(entity);
            return new ResponseModel<ProductAttributeDto>(dto, 200);
        }
    }
}
