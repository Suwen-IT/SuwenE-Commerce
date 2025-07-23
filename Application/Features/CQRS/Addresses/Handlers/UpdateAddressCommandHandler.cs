using Application.Common.Models;
using Application.Features.CQRS.Addresses.Commands;
using Application.Features.DTOs.Addresses;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Addresses.Handlers;

public class UpdateAddressCommandHandler:IRequestHandler<UpdateAddressCommandRequest,ResponseModel<AddressDto>>
{
    private readonly IReadRepository<Address> _readRepository;
    private readonly IWriteRepository<Address> _writeRepository;
    private readonly IMapper _mapper;

    public UpdateAddressCommandHandler(IReadRepository<Address> readRepository,
        IWriteRepository<Address> writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }
    
    public async Task<ResponseModel<AddressDto>> Handle(UpdateAddressCommandRequest request, CancellationToken cancellationToken)
    {
        var existingAddress = await _readRepository.GetByIdAsync(request.Id);
        
        if(existingAddress == null)
            return new ResponseModel<AddressDto>("Güncellenecek adres bulunamadı.",404);
        
        _mapper.Map(request, existingAddress);
        
        await _writeRepository.UpdateAsync(existingAddress);
        var success = await _writeRepository.SaveChangesAsync();

        if (!success)
            return new ResponseModel<AddressDto>("Adres güncellenmedi.", 500);
        
        var addressDto = _mapper.Map<AddressDto>(existingAddress);
        return new ResponseModel<AddressDto>(addressDto,200);
    }
}