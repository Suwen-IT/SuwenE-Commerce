using Application.Common.Models;
using Application.Features.DTOs.Addresses;
using Application.Interfaces.Validations;
using MediatR;

namespace Application.Features.CQRS.Addresses.Commands;

public class UpdateAddressCommandRequest:IRequest<ResponseModel<AddressDto>>,IAddressCommandBase
{
    public int Id { get; set; }
    
    public string Title { get; set; }=string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    
    public Guid AppUserId { get; set; }
}