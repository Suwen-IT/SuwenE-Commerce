﻿using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Users.Commands
{
    public class LogoutCommandRequest:IRequest<ResponseModel<object>>
    {
    }
}
