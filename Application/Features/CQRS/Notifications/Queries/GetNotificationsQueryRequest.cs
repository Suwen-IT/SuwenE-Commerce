using Application.Common.Models;
using Application.Features.DTOs.Notifications;
using MediatR;

namespace Application.Features.CQRS.Notifications.Queries;

public class GetNotificationsQueryRequest:IRequest<ResponseModel<List<NotificationDto>>>
{
    public Guid UserId { get; set; }
}