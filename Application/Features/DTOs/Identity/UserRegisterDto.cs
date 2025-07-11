using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs.Identity
{
    public class UserRegisterDto
    {
        public string[]? Errors { get; set; }
        public bool IsSuccess { get; set; } 
        public Guid UserId { get; set; }
        public string Role { get; set; } 
        



}
}
