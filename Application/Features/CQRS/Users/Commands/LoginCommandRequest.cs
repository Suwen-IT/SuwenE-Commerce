using Application.Common.Models;
using Application.Features.DTOs.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Users.Commands
{
    public class LoginCommandRequest:IRequest<ResponseModel<UserLoginDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
