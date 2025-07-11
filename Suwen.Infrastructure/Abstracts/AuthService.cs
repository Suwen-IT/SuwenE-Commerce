using Application.Common.Models;
using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using Application.Interfaces;
using Application.Services;
using Domain.Constants;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<UserLoginDto> LoginAsync(LoginCommandRequest loginCommandRequest)
        {
            if (loginCommandRequest is null)
                throw new NullReferenceException($"{nameof(LoginCommandRequest)} is null!");

            AppUser? user = await _userManager.FindByEmailAsync(loginCommandRequest.Email);

            if (user is null || user.IsDeleted is true)
            {
                return new()
                {
                    Errors = new string[] { "User not found" },
                    Role = string.Empty,
                    Token = string.Empty,
                    UserDto = new()
                };
            }

            bool checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginCommandRequest.Password);

            if (checkPasswordResult is false)
            {
                return new()
                {
                    Errors = new[] { "Invalid email or password!" },
                    Role = string.Empty,
                    Token = string.Empty,
                    UserDto = new()
                };
            }

            IList<string> userRole = await _userManager.GetRolesAsync(user);

            Token? token = _tokenService.GenerateToken(user);

            return new()
            {
                UserDto = new()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    CreatedAtUtc = user.CreatedTime
                },
                Role = userRole.FirstOrDefault(),
                Errors = new string[] { },
                Token = token.AccessToken
            };
        }


        public async Task<UserRegisterDto> RegisterAsync(RegisterCommandRequest registerCommandRequest, string? role = "User")
        {
            if (registerCommandRequest == null)
                throw new NullReferenceException($"{nameof(registerCommandRequest)}is null");

            AppUser appUser = new()
            {
                Name = registerCommandRequest.Name ?? string.Empty,
                Surname = registerCommandRequest.Surname ?? string.Empty,
                Email = registerCommandRequest.Email ?? string.Empty,
                PhoneNumber = registerCommandRequest.PhoneNumber ?? string.Empty,
                UserName=registerCommandRequest.UserName??string.Empty
              
            };

            IdentityResult userResult = await _userManager.CreateAsync(appUser, registerCommandRequest.Password);

            if (userResult.Succeeded)
            {

                bool userRoleExist = await _roleManager.RoleExistsAsync(role);
                if (!userRoleExist)
                {
                    IdentityResult roleResult = await _roleManager.CreateAsync(new AppRole { Name = role });
                    if (!roleResult.Succeeded)
                    {
                        return new()
                        {
                            IsSuccess = false,
                            Errors = roleResult.Errors.Select(e => e.Description).ToArray()
                        };
                    }

                }
            IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(appUser, role);
            if (addToRoleResult.Succeeded)
            {
                return new()
                {
                    IsSuccess = true,
                    Role = role,
                    UserId = appUser.Id,
                    Errors = addToRoleResult.Errors.Select(e => e.Description).ToArray()
                };
            }
            else 
            {
                return new()
                {
                    IsSuccess = false,
                    Role = role,
                    UserId = appUser.Id,
                    Errors = addToRoleResult.Errors.Select(e => e.Description).ToArray()
                };
            }

            }
            else
            {
                return new()
                {
                    Errors = userResult.Errors.Select(e => e.Description).ToArray(),
                    IsSuccess = false,
                    Role = role
                };
            }
        }

    }
}