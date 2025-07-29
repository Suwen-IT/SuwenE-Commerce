using Application.Common.Models;
using Application.Features.DTOs.Addresses;
using MediatR;

namespace Application.Features.CQRS.Addresses.Queries;

public class GetAddressByIdQueryRequest:IRequest<ResponseModel<AddressDto>>
{
    public int Id { get; set; }
    public Guid AppUserId { get; set; }

}