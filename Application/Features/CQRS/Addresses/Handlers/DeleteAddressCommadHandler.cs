using Application.Common.Models;
using Application.Features.CQRS.Addresses.Commands;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Addresses.Handlers;

public class DeleteAddressCommadHandler:IRequestHandler<DeleteAddressCommandRequest, ResponseModel<NoContent>>
{
    private readonly IReadRepository<Address> _readRepository;
    private readonly IWriteRepository<Address> _writeRepository;

    public DeleteAddressCommadHandler(IReadRepository<Address> readRepository,
        IWriteRepository<Address> writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }
    
    public async Task<ResponseModel<NoContent>> Handle(DeleteAddressCommandRequest request, CancellationToken cancellationToken)
    {
        var existingAddress =await _readRepository.GetByIdAsync(request.Id);
        if (existingAddress == null)
            return new ResponseModel<NoContent>("Silinecek adres bulunamadı.", 404);
        
        _writeRepository.DeleteAsync(existingAddress);
        var success = await _writeRepository.SaveChangesAsync();

        if (!success)
            return new ResponseModel<NoContent>("Adres silinirken bir sorun oluştu.", 500);

        return new ResponseModel<NoContent>(new NoContent(),204);
    }
}