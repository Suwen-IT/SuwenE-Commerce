using Application.Common.Models;
using MediatR;

namespace Application.Features.CQRS.Notifications.Commands;

public class MarkAsReadCommandRequest:IRequest<ResponseModel<NoContent>>
{
    public int NotificationId { get; set; }
}