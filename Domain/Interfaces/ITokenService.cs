using Domain.Constants;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ITokenService
    {
        Token GenerateToken(AppUser appUser);   
    }
}
