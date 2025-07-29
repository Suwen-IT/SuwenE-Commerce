using Application.Common.Models;
using Application.Features.CQRS.Addresses.Commands;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Addresses.Handlers;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommandRequest, ResponseModel<NoContent>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAddressCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModel<NoContent>> Handle(DeleteAddressCommandRequest request, CancellationToken cancellationToken)
    {
        var address = await _unitOfWork.GetReadRepository<Address>().GetByIdAsync(request.Id);
        if (address == null)
            return new ResponseModel<NoContent>("Silinecek adres bulunamadı.", 404);

        await _unitOfWork.GetWriteRepository<Address>().DeleteAsync(address);

        var success = await _unitOfWork.SaveChangesBoolAsync();

        if (!success)
            return new ResponseModel<NoContent>("Adres silinirken bir sorun oluştu.", 500);

        return new ResponseModel<NoContent>(new NoContent(), 204);
    }
}