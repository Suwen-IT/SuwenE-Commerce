using Application.Common.Models;
using Application.Features.DTOs.Notifications;
using Domain.Entities.Enums;
using MediatR;

namespace Application.Features.CQRS.Notifications.Commands;

public class CreateNotificationCommandRequest:IRequest<ResponseModel<NotificationDto>>
{
    public Guid AppUserId { get; set; }
    public string Title { get; set; }=string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; } = NotificationType.SystemOnly;
}