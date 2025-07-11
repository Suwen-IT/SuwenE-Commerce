using Application.Features.DTOs.Identity;
using AutoMapper;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class UserMap:Profile
    {
        public UserMap() 
        {
            CreateMap<AppUser, UserDto>()
                .ReverseMap();
        }
    }
}
