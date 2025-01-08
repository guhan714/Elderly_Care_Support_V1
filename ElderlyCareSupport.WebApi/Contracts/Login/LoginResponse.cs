using Newtonsoft.Json;

namespace ElderlyCareSupport.Server.Contracts.Login;

public record LoginResponse(

    [JsonProperty(nameof(AccessToken))] string AccessToken,
    [JsonProperty(nameof(ExpiresIn))] int ExpiresIn, 
    [JsonProperty(nameof(RefreshToken))] string RefreshToken 
);

