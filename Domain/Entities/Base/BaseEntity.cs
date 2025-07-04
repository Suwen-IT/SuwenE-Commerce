using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
    public abstract class BaseEntity:IBaseEntity
    {
        public int Id { get; set; } 
        public DateTime CreatedTime { get; set; }=DateTime.UtcNow;
    
        public bool IsDeleted { get; set; }=false; 
    }
}
