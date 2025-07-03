using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs.Addresses
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Tittle { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Guid UserId { get; set; }
    }
}
