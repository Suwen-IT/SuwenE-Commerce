using Domain.Entities.Base;
using Domain.Entities.Enums;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notification: BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; } = NotificationType.SystemOnly;
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
