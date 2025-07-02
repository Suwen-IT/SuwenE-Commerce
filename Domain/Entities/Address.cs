using Domain.Base;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address: BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public string Tittle { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
