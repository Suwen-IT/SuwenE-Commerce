using Application.Common.Models;
using Application.Features.CQRS.Addresses.Queries;
using Application.Features.DTOs.Addresses;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;

public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQueryRequest, ResponseModel<AddressDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAddressByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<AddressDto>> Handle(GetAddressByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var address = await _unitOfWork
            .GetReadRepository<Address>()
            .GetAsync(a => a.Id == request.Id && a.AppUserId == request.AppUserId, 
                enableTracking: false);

        if (address == null)
            return new ResponseModel<AddressDto>("Adres bulunamadı.", 404);

        var addressDto = _mapper.Map<AddressDto>(address);
        return new ResponseModel<AddressDto>(addressDto, 200);
    }
}