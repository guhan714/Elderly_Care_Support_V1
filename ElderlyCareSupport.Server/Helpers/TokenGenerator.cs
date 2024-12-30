using ElderlyCareSupport.Server.ResponseModels;
using ElderlyCareSupport.Server.Services.Interfaces;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace ElderlyCareSupport.Server.Helpers
{
    public class TokenGenerator : ITokenService
    {
        private readonly string _issuer;
        private readonly string _clientId;
        private readonly string _secretKey;
        private readonly HttpClient _httpClient;
        private readonly ILogger<TokenGenerator> _logger;
        public TokenGenerator(IConfiguration configuration, ILogger<TokenGenerator> logger, HttpClient httpClient)
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
                    new DiscoveryDocumentRequest()
                    {
                        Address = _issuer,
                        Policy = { RequireHttps = false }
                    });

                if (client.IsError)
                {
                    _logger.LogError("Discovery Failed");
                    return string.Empty;
                }

                var tokenRequest = new ClientCredentialsTokenRequest()
                {
                    Address = client.TokenEndpoint,
                    ClientId = _clientId,
                    ClientSecret = _secretKey,
                    ClientCredentialStyle = ClientCredentialStyle.AuthorizationHeader,
                    GrantType = "authorization_code",  // Use 'authorization_code' flow
                };

                var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(tokenRequest);

                if (!tokenResponse.IsError) return JsonConvert.SerializeObject(tokenResponse);
                
                _logger.LogError(tokenResponse.ErrorDescription ?? tokenResponse.Exception?.Message); 
                return string.Empty;

            }

            catch (Exception ex)
            {
                _logger.LogError("Exception occurred. {Exception}.", ex.Message);
                return null;
            }
        }

        public LoginResponse? GenerateToken()
        {
            try
            {
                var token = ConfigureToken().Result;
                return !string.IsNullOrEmpty(token) ? JsonConvert.DeserializeObject<LoginResponse>(token) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception has been occurred : {Exception}", ex.Message);
                return null;
            }  
        }

       
    }
}


