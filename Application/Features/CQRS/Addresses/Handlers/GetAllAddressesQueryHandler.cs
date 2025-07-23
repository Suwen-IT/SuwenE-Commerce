using Application.Common.Models;
using Application.Features.CQRS.Addresses.Queries;
using Application.Features.DTOs.Addresses;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Addresses.Handlers;

public class GetAllAddressesQueryHandler:IRequestHandler<GetAllAddressesQueryRequest,ResponseModel<List<AddressDto>>>
{
    private readonly IReadRepository<Address> _repository;
    private readonly IMapper _mapper;

    public GetAllAddressesQueryHandler(IReadRepository<Address> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseModel<List<AddressDto>>> Handle(GetAllAddressesQueryRequest request, CancellationToken cancellationToken)
    {
        var addresses = await _repository.GetAllAsync(a => a.AppUserId == request.AppUserId);
        
        if(addresses==null || !addresses.Any())
            return new ResponseModel<List<AddressDto>>(new List<AddressDto>(), 200);
        
        var dtoList = _mapper.Map<List<AddressDto>>(addresses);
        return new ResponseModel<List<AddressDto>>(dtoList, 200);
    }
}