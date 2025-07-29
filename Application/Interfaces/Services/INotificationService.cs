using Domain.Entities;
using Domain.Entities.Enums;

namespace Application.Interfaces.Services;

public interface INotificationService
{
    Task<Notification> CreateNotificationAsync(Guid userId,string title, string message ,NotificationType type);
    Task<List<Notification>> GetNotificationsAsync(Guid userId);
    Task MarkAsReadAsync(int notificationId);
}