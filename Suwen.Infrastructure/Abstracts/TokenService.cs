using Application.Services;
using Domain.Constants;
using Domain.Entities.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{

    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;

        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public Token GenerateToken(AppUser user)
        {

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            new Claim("Email",user.Id.ToString()),
            new Claim("userId", user.Id.ToString()),
        };

            // 2. Şifreleme anahtarını oluştur
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var signingCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Token ayarları
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
                signingCredentials: signingCredential
            );



            // 4. Token'ı paketle
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new Token
            {
                AccessToken = accessToken,
                Expiration = token.ValidTo,
                RefreshToken = null
            };
        }
    }
}