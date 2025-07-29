using Domain.Entities.Enums;


namespace Application.Features.DTOs.Notifications
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public string Message { get; set; }=string.Empty;
        public NotificationType Type { get; set; }
        public Guid AppUserId { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
    }
}
