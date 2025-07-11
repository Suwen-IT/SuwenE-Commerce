using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs.Identity
{
    public class UserLoginDto
    {
        public string[]? Errors { get; set; }
        public string Role { get; set; }
        public string? Token { get; set; }
        public UserDto UserDto { get; set; }
    }
}
