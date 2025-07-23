using Application.Common.Models;
using Application.Features.CQRS.Addresses.Queries;
using Application.Features.DTOs.Addresses;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Addresses.Handlers;

public class GetAddressByIdQueryHandler:IRequestHandler<GetAddressByIdQueryRequest, ResponseModel<AddressDto>>
{
    private readonly IReadRepository<Address> _readRepository;
    private readonly IMapper _mapper;

    public GetAddressByIdQueryHandler(IReadRepository<Address> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }
    public async Task<ResponseModel<AddressDto>> Handle(GetAddressByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var address = await _readRepository.GetAsync(a => a.Id == request.Id && a.AppUserId == request.AppUserId);
        if (address == null)
            return new ResponseModel<AddressDto>("Adres bulunamadı.",404);
        
        var addressDto = _mapper.Map<AddressDto>(address);
        return new ResponseModel<AddressDto>(addressDto,200);
    }
}