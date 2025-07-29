using Application.Common.Models;
using MediatR;

namespace Application.Features.CQRS.Addresses.Commands;

public class DeleteAddressCommandRequest:IRequest<ResponseModel<NoContent>>
{
    public int Id { get; set; }
    public Guid AppUserId { get; set; }
}