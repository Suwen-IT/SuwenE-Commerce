using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Entities.Identity;
using Microsoft.Extensions.Logging;

namespace Suwen.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<NotificationService> _logger;
    private readonly IEmailSender _emailSender;
    private readonly ITemplateService _templateService;

    public NotificationService(
        IUnitOfWork unitOfWork,
        ILogger<NotificationService> logger,
        IEmailSender emailSender,
        ITemplateService templateService) 
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _emailSender = emailSender;
        _templateService = templateService;
    }

    public async Task<Notification> CreateNotificationAsync(Guid userId, string title, string message, NotificationType type)
    {
        var notification = new Notification
        {
            AppUserId = userId,
            Title = title,
            Message = message,
            Type = type,
            SentDate = DateTime.Now
        };

        await _unitOfWork.GetWriteRepository<Notification>().AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Yeni bildirim oluşturuldu: {NotificationId}", notification.Id);

        try
        {
            var user = await _unitOfWork.GetReadRepository<AppUser>().GetByIdAsync(userId);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                var template = await _templateService.LoadTemplateAsync("WelcomeTemplate.html");
                
                var parameters = new Dictionary<string, string>
                {
                    { "FirstName", user.FirstName ?? " "},
                    { "LastName", user.LastName ?? " "},
                    { "Message", message }
                };
                
                var emailBody = _templateService.ParseTemplate(template, parameters);
                
                await _emailSender.SendEmailAsync(user.Email, title, emailBody);

                _logger.LogInformation("Bildirim e-posta ile gönderildi: {Email}", user.Email);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bildirim e-posta gönderiminde hata oluştu.");
        }

        return notification;
    }

    public async Task<List<Notification>> GetNotificationsAsync(Guid userId)
    {
        return await _unitOfWork.GetReadRepository<Notification>().GetAllAsync(n => n.AppUserId == userId);
    }

    public async Task MarkAsReadAsync(int notificationId)
    {
        var notification = await _unitOfWork.GetReadRepository<Notification>().GetByIdAsync(notificationId);
        if (notification == null)
            throw new KeyNotFoundException("Bildirim bulunamadı.");

        notification.IsRead = true;
        await _unitOfWork.GetWriteRepository<Notification>().UpdateAsync(notification);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Bildirim okundu: {NotificationId}", notificationId);
    }
}
