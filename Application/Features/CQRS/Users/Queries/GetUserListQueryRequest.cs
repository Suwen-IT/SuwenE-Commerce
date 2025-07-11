using Application.Common.Models;
using Application.Features.DTOs.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Users.Queries
{
    public class GetUserListQueryRequest:IRequest <ResponseModel<List<UserDto>>>
    {

    }
}
