﻿namespace Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
            string? UserId { get; }
            string? UserName { get; }
            bool IsAuthenticated { get; }

        
    }

}

