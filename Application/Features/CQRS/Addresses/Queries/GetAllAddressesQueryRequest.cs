using Application.Common.Models;
using Application.Features.DTOs.Addresses;
using MediatR;

namespace Application.Features.CQRS.Addresses.Queries;

public class GetAllAddressesQueryRequest:IRequest<ResponseModel<List<AddressDto>>>
{
    public Guid AppUserId { get; set; }

    public GetAllAddressesQueryRequest(Guid appUserId)
    {
        AppUserId = appUserId;
    }
}