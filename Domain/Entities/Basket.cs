using Domain.Entities.Base;
using Domain.Entities.Identity;


namespace Domain.Entities
{
    public class Basket:BaseEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
        public ICollection<BasketItem>BasketItems { get; set; }=new HashSet<BasketItem>();
    }
}
