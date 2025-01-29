using ElderlyCareSupport.Application.Contracts.Response;
using ElderlyCareSupport.Application.IService;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ElderlyCareSupport.Application.Helpers
{
    public class JwtTokenGenerator : ITokenService
    {
        private readonly string _issuer;
        private readonly string _clientId;
        private readonly string _secretKey;
        private readonly HttpClient _httpClient;
        private readonly ILogger<JwtTokenGenerator> _logger;
        public JwtTokenGenerator(IConfiguration configuration, ILogger<JwtTokenGenerator> logger, HttpClient httpClient)
        {
            var data = configuration.GetSection("JWT");
            _issuer = data["Issuer"]!;
            _clientId = data["ClientId"]!;
            _secretKey = data["SecretKey"]!;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<string?> ConfigureToken()
        {
            try
            {
                var client = await _httpClient.GetDiscoveryDocumentAsync(
                    new DiscoveryDocumentRequest
                    {
                        Address = _issuer,
                        Policy = { RequireHttps = false }
                    });

                if (client.IsError)
                {
                    _logger.LogError("Discovery Failed");
                    return string.Empty;
                }

                var tokenRequest = new ClientCredentialsTokenRequest
                {
                    Address = client.TokenEndpoint,
                    ClientId = _clientId,
                    ClientSecret = _secretKey,
                    GrantType = "authorization_code",
                    Scope = "openid offline_access"
                };

                var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(tokenRequest);

                if(tokenResponse.IsError)
                    _logger.LogError("Token Request Failed. Error: {ErrorDescription}, Exception: {Exception}",
                        tokenResponse.ErrorDescription, tokenResponse.Exception?.ToString());

                return JsonConvert.SerializeObject(tokenResponse);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred. {Exception}.", ex.Message);
                return string.Empty;
            }
        }

        public async Task<LoginResponse?> GenerateToken()
        {
            try
            {
                var token = await ConfigureToken();
                return JsonConvert.DeserializeObject<LoginResponse>(token!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception has been occurred : {Exception}", ex.Message);
                return null;
            }
        }


    }
}


