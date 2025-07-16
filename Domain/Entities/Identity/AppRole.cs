using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class AppRole:IdentityRole<Guid>
    {
        public DateTime? CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedTime { get; set; }
    }
}
