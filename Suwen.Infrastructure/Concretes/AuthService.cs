using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using Application.Interfaces;
using Application.Services;
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

        public async Task<AuthResponseDto> LoginAsync(LoginCommandRequest loginCommandRequest)
        {
            if (loginCommandRequest is null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "LoginRequest connot be null!" }
                };
            }
            
            AppUser? user = await _userManager.FindByEmailAsync(loginCommandRequest.Email);

            if (user is null )
            {
                return new()
                {
                   IsSuccess = false,
                   Errors = new List<string> { "Invalid credentials!" }
                };
            }
            
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginCommandRequest.Password, false);
            if (!signInResult.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Invalid credentials!" }
                };
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);
            Token? token = _tokenService.GenerateToken(user);

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
                    Errors = new List<string> { "RegisterRequest connot be null!" }
                };
            }
            var errors = new List<string>();

            if (await _userManager.FindByEmailAsync(registerCommandRequest.Email) != null)
            {
              errors.Add("Email is already in use!");
            }

            if (await _userManager.FindByNameAsync(registerCommandRequest.UserName) != null)
            {
                errors.Add("Username is already taken!");
            }
            
            var userWithPhoneNumber = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == registerCommandRequest.PhoneNumber);
            if (userWithPhoneNumber != null)
            {
                errors.Add("Phone number is already in use!");
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