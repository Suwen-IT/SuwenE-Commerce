using Domain.Entities.Base;
using Domain.Entities.Enums;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class Notification: BaseEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = default!;
        
        public string Title { get; set; }=string.Empty;
        public string Message { get; set; }=string.Empty;
        public NotificationType Type { get; set; } = NotificationType.SystemOnly;
        public DateTime SentDate { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;

    }
}
