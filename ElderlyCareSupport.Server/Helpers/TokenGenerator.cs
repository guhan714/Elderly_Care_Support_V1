using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;

namespace ElderlyCareSupport.Server.Helpers
{
    public class TokenGenerator : ITokenService
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secretKey;
        private readonly IConfigurationSection data;
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly IClock clock;
        private readonly ILogger<TokenGenerator> logger;
        public TokenGenerator(IConfiguration configuration, IClock clock, ILogger<TokenGenerator> logger)
        {
            data = configuration.GetSection("JWT");
            _issuer = data["Issuer"]!;
            _audience = data["Audience"]!;
            _secretKey = data["SecretKey"]!;
            tokenHandler = new();
            this.clock = clock;
            this.logger = logger;
        }

        public SecurityTokenDescriptor ConfigureJWTToken(string userName)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(_secretKey);
                SigningCredentials signingCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = new ClaimsIdentity([new Claim(ClaimTypes.Name, userName)]),
                    Issuer = _issuer,
                    Audience = _audience,
                    IssuedAt = clock.GetDateTime(),
                    Expires = clock.GetDateTime().AddMinutes(15),
                    SigningCredentials = signingCredentials,
                };

                return tokenDescriptor;
            }

            catch (Exception ex)
            {
                logger.LogError("Exception occurred. {Exception}.", ex.Message);
                return null;
            }
        }

        public string GenerateJWTToken(string userName)
        {
            try
            {
                var token = tokenHandler.CreateToken(ConfigureJWTToken(userName));
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                return string.Empty;
            }  
        }

       
    }
}


