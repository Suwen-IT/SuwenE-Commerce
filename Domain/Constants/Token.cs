﻿
namespace Domain.Constants
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string? RefreshToken { get; set; }
        
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
