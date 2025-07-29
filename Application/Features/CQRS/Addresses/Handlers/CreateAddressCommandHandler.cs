using Application.Common.Models;
using Application.Features.CQRS.Addresses.Commands;
using Application.Features.DTOs.Addresses;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;
using MediatR;

namespace Application.Features.CQRS.Addresses.Handlers;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommandRequest, ResponseModel<AddressDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<AddressDto>> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
    {
        var appUser = await _unitOfWork.GetReadRepository<AppUser>().GetByIdAsync(request.AppUserId);
        if (appUser == null)
            return new ResponseModel<AddressDto>("Belirtilen kullanıcı bulunamadı.", 400);

        var address = _mapper.Map<Address>(request);

        await _unitOfWork.GetWriteRepository<Address>().AddAsync(address);
        var saved = await _unitOfWork.SaveChangesBoolAsync();

        if (!saved)
            return new ResponseModel<AddressDto>("Adres oluşturulurken bir hata oluştu.", 500);

        var addressDto = _mapper.Map<AddressDto>(address);
        return new ResponseModel<AddressDto>(addressDto, 201);
    }
}