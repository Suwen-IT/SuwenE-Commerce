using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;



namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterCommandRequest registerCommandRequest, string? role = "User");
        Task<AuthResponseDto> LoginAsync(LoginCommandRequest loginCommandRequest);

        Task<bool>InvalidateRefreshTokenAsync(string userId);
    }
}
