using Application.Common.Models;
using Application.Features.CQRS.ProductAttributeValues.Queries;
using Application.Features.DTOs;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

public class GetAllProductAttributeValuesQueryHandler : IRequestHandler<GetAllProductAttributeValuesQueryRequest, ResponseModel<List<ProductAttributeValueDto>>>
{
    private readonly IReadRepository<ProductAttributeValue> _readRepository;
    private readonly IMapper _mapper;

    public GetAllProductAttributeValuesQueryHandler(IReadRepository<ProductAttributeValue> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<ResponseModel<List<ProductAttributeValueDto>>> Handle(GetAllProductAttributeValuesQueryRequest request, CancellationToken cancellationToken)
    {
        var list = await _readRepository.GetAllAsync();

        var dtos = _mapper.Map<List<ProductAttributeValueDto>>(list);
        return new ResponseModel<List<ProductAttributeValueDto>>(dtos, 200);
    }
}