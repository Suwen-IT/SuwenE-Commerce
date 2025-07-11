using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserRegisterDto> RegisterAsync(RegisterCommandRequest registerCommandRequest, string? role = "User");
        Task<UserLoginDto> LoginAsync(LoginCommandRequest loginCommandRequest);
    }
}
