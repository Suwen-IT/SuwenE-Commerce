using Domain.Constants;
using Domain.Entities.Identity;


namespace Application.Services
{
    public interface ITokenService
    {
        Task<Token> GenerateToken(AppUser appUser);
    }
}
