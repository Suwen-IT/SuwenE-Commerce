﻿using Application.Features.CQRS.Users.Commands;
using Application.Features.DTOs.Identity;
using AutoMapper;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;


namespace Application.Mappers
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterCommandRequest, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName ?? src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName)) 
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
            

            CreateMap<AppUser, AuthResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokenExpiration, opt => opt.Ignore())
                .ForMember(dest => dest.IsSuccess, opt => opt.Ignore())
                .ForMember(dest => dest.Errors, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
            
            CreateMap<AppUser,UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest=>dest.FirstName, opt=>opt.MapFrom(src=>src.FirstName))
                .ForMember(dest=>dest.LastName, opt=>opt.MapFrom(src=>src.LastName))
                .ForMember(dest=>dest.PhoneNumber, opt=>opt.MapFrom(src=>src.PhoneNumber))
                .ForMember(dest=> dest.EmailConfirmed, opt=>opt.MapFrom(src=>src.EmailConfirmed))
                .ForMember(dest=>dest.CreatedAtUtc,opt=>opt.MapFrom(src=>src.CreatedDate))
                .ForMember(dest=>dest.Roles, opt=>opt.Ignore());

            CreateMap<AppUser, UserUpdateDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest=>dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest=>dest.Email,opt=> opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<UpdateUserCommandRequest,AppUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName, opt => opt.Condition(src=>src.FirstName!=null))
                .ForMember(dest => dest.LastName, opt => opt.Condition(src=>src.LastName!=null))
                .ForMember(dest => dest.Email, opt => opt.Condition(src=>src.Email !=null))
                .ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src=>src.PhoneNumber !=null))
                .ForMember(dest => dest.UserName, opt => opt.Condition(src=>src.UserName !=null));


        }
    }
}
