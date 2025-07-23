using Domain.Entities;
using Domain.Entities.Identity;

namespace Application.Features.DTOs.Reviews
{
    public class ReviewDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }=string.Empty;

        public Guid AppUserId { get; set; }
        public string AppUserName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
