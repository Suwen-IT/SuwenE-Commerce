using Application.Features.DTOs.Addresses;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class AddressMap:Profile
    {
        public AddressMap() 
        {
            CreateMap<Address, AddressDto>();
        }

    }
}
