using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Constants;
using Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;


namespace Suwen.Infrastructure.Abstracts
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<bool> InvalidateRefreshTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false; 
            }
            user.RefreshToken = null;
            user.RefreshTokenExpiration = null;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginCommandRequest loginCommandRequest)
        {
            if (loginCommandRequest is null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Giriş isteği boş olamaz!" }
                };
            }
            
            AppUser? user = await _userManager.FindByEmailAsync(loginCommandRequest.Email);

            if (user is null )
            {
                return new()
                {
                   IsSuccess = false,
                   Errors = new List<string> { "Geçersiz kimlik bilgleri!" }
                };
            }
            
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginCommandRequest.Password, false);
            if (!signInResult.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Geçersiz kimlik bilgleri!" }
                };
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);
            Token? token = await _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                IsSuccess = true,
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                Email = user.Email,
                Token = token?.AccessToken,
                RefreshToken = token?.RefreshToken,
                RefreshTokenExpiration = token?.RefreshTokenExpiration,
                Roles = roles.ToList(),
                Errors = new List<string>()
            };
        }


        public async Task<AuthResponseDto> RegisterAsync(RegisterCommandRequest registerCommandRequest, string? role = "User")
        {
            if (registerCommandRequest == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Kullanıcı kayıt işlemi boş olamaz!" }
                };
            }
            var errors = new List<string>();

            if (await _userManager.FindByEmailAsync(registerCommandRequest.Email) != null)
            {
              errors.Add("E-posta adresi kullanımdadır.");
            }

            if (await _userManager.FindByNameAsync(registerCommandRequest.UserName) != null)
            {
                errors.Add("Kullanıcı adı daha önce alınmıştır");
            }
            
            var userWithPhoneNumber = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == registerCommandRequest.PhoneNumber);
            if (userWithPhoneNumber != null)
            {
                errors.Add("Telefon numarası kullanımdadır.");
            }
            if (errors.Any())
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = errors
                };
            }

            AppUser appUser = new()
            {
                Email = registerCommandRequest.Email ?? string.Empty,
                PhoneNumber = registerCommandRequest.PhoneNumber ?? string.Empty,
                UserName=registerCommandRequest.UserName??string.Empty,
                FirstName = registerCommandRequest.FirstName ?? string.Empty,
                LastName = registerCommandRequest.LastName ?? string.Empty
            };

            IdentityResult userCreationResult = await _userManager.CreateAsync(appUser, registerCommandRequest.Password);

            if (userCreationResult.Succeeded)
            {
                bool roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult roleCreationResult = await _roleManager.CreateAsync(new AppRole { Name = role });
                    if (!roleCreationResult.Succeeded)
                    {
                        return new()
                        {
                            IsSuccess = false,
                            Errors = roleCreationResult.Errors.Select(e => e.Description).ToList()
                        };
                    }

                }
                
            IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(appUser, role);
            if (addToRoleResult.Succeeded)
            {
                return new()
                {
                    IsSuccess = true,
                    UserId = appUser.Id.ToString(),
                    UserName = appUser.UserName,
                    Email=appUser.Email,
                    Roles=new List<string>{role},
                    Errors = new List<string>()
                };
            }
            else 
            {
                return new()
                {
                    IsSuccess = false,
                    Errors = addToRoleResult.Errors.Select(e => e.Description).ToList()
                };
            } 
            }
            else
            {
                return new()
                {
                    IsSuccess = false,
                    Errors = userCreationResult.Errors.Select(e => e.Description).ToList()
                    
                };
            }
        }

    }
}