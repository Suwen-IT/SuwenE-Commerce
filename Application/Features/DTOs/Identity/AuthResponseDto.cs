namespace Application.Features.DTOs.Identity;

public class AuthResponseDto
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    public bool IsSuccess { get; set; }
    public List<string>? Errors { get; set; }
    public List<string>? Roles { get; set; }
}