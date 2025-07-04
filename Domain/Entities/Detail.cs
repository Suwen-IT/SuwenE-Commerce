using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Detail:BaseEntity
    {
     
        public  string Title { get; set; }
        public  string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
