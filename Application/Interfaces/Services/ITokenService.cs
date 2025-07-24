using Domain.Constants;
using Domain.Entities.Identity;


namespace Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<Token> GenerateToken(AppUser appUser);
    }
}
