using Application.Common.Models;
using Application.Features.CQRS.ProductAttributes.Queries;
using Application.Features.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.ProductAttributes.Handlers
{
    public class GetAllProductAttributesQueryHandler : IRequestHandler<GetAllProductAttributesQueryRequest, ResponseModel<List<ProductAttributeDto>>>
    {
        private readonly IReadRepository<ProductAttribute> _repository;
        private readonly IMapper _mapper;

        public GetAllProductAttributesQueryHandler(IReadRepository<ProductAttribute> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ProductAttributeDto>>> Handle(GetAllProductAttributesQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync();

            if(list == null||!list.Any())
                return new ResponseModel<List<ProductAttributeDto>>("Ürün niteliği bulunmamaktadır.", 404);

            var dtoList=_mapper.Map<List<ProductAttributeDto>>(list);
            return new ResponseModel<List<ProductAttributeDto>>(dtoList, 200);

        }
    }
}
