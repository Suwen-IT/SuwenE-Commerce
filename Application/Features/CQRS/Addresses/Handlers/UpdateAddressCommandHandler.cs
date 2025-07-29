using Application.Common.Models;
using Application.Features.CQRS.Addresses.Commands;
using Application.Features.DTOs.Addresses;
using Application.Interfaces.UnitOfWorks;
using AutoMapper;
using Domain.Entities;
using MediatR;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommandRequest, ResponseModel<AddressDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<AddressDto>> Handle(UpdateAddressCommandRequest request, CancellationToken cancellationToken)
    {
        var existingAddress = await _unitOfWork.GetReadRepository<Address>().GetByIdAsync(request.Id);
        if (existingAddress == null)
            return new ResponseModel<AddressDto>("Güncellenecek adres bulunamadı.", 404);

        _mapper.Map(request, existingAddress);

        await _unitOfWork.GetWriteRepository<Address>().UpdateAsync(existingAddress);

        var success = await _unitOfWork.SaveChangesBoolAsync();

        if (!success)
            return new ResponseModel<AddressDto>("Adres güncellenemedi.", 500);

        var addressDto = _mapper.Map<AddressDto>(existingAddress);
        return new ResponseModel<AddressDto>(addressDto, 200);
    }
}

