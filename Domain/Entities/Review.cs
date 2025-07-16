using Domain.Entities.Base;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class Review: BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
    
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime ReviewDate { get; set; }=DateTime.UtcNow;
}