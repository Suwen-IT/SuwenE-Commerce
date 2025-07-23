using Application.Common.Models;
using Application.Features.CQRS.Addresses.Commands;
using Application.Features.DTOs.Addresses;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Addresses.Handlers;

public class CreateAddressCommandHandler:IRequestHandler<CreateAddressCommandRequest,ResponseModel<AddressDto>>
{
    private readonly IWriteRepository<Address> _repository;
    private readonly IMapper _mapper;

    public CreateAddressCommandHandler(IWriteRepository<Address> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseModel<AddressDto>> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
    {
        var address = _mapper.Map<Address>(request);
        
        await _repository.AddAsync(address);
        var success = await _repository.SaveChangesAsync();
        
        if(!success)
            return new ResponseModel<AddressDto>("Adres oluşturulamadı.",500);
        
        var addressDto = _mapper.Map<AddressDto>(address);
        return new ResponseModel<AddressDto>(addressDto,201);
    }
}