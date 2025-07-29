using Application.Common.Models;
using Application.Features.CQRS.Addresses.Queries;
using Application.Features.DTOs.Addresses;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;

public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQueryRequest, ResponseModel<List<AddressDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllAddressesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<List<AddressDto>>> Handle(GetAllAddressesQueryRequest request, CancellationToken cancellationToken)
    {
        var addresses = await _unitOfWork
            .GetReadRepository<Address>()
            .GetAllAsync(a => a.AppUserId == request.AppUserId, enableTracking: false);

        if (!addresses.Any())
            return new ResponseModel<List<AddressDto>>(new List<AddressDto>(), 200);

        var dtoList = _mapper.Map<List<AddressDto>>(addresses);
        return new ResponseModel<List<AddressDto>>(dtoList, 200);
    }
}