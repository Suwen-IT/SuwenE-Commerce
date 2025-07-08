
namespace Domain.Entities.Base
{
    public abstract class BaseEntity:IBaseEntity
    {
        public int Id { get; set; } 
        public DateTime CreatedTime { get; set; }=DateTime.UtcNow;
        public bool IsDeleted { get; set; }=false; 
    }
}
