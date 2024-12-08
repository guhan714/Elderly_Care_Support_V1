using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElderlyCareSupport.Server.Helpers
{
    public class TokenGenerator : ITokenService
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secretKey;
        private readonly JwtSecurityTokenHandler _tokenHandler = new();
        private readonly IClock _clock;
        private readonly ILogger<TokenGenerator> _logger;
        public TokenGenerator(IConfiguration configuration, IClock clock, ILogger<TokenGenerator> logger)
        {
            var data = configuration.GetSection("JWT");
            _issuer = data["Issuer"]!;
            _audience = data["Audience"]!;
            _secretKey = data["SecretKey"]!;
            _clock = clock;
            _logger = logger;
        }

        public SecurityTokenDescriptor? ConfigureJwtToken(string userName)
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
                    IssuedAt = _clock.GetDateTime(),
                    Expires = _clock.GetDateTime().AddMinutes(15),
                    SigningCredentials = signingCredentials
                };

                return tokenDescriptor;
            }

            catch (Exception ex)
            {
                _logger.LogError("Exception occurred. {Exception}.", ex.Message);
                return null;
            }
        }

        public string GenerateJwtToken(string userName)
        {
            try
            {
                var token = _tokenHandler.CreateToken(ConfigureJwtToken(userName));
                return _tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                return string.Empty;
            }  
        }

       
    }
}


