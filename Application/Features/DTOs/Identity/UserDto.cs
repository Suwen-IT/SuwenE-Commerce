using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs.Identity
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMalformed { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
        public DateTime? CreatedAtUtc { get; set; }
    }
}
