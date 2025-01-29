using System.Text.Json.Serialization;

namespace ElderlyCareSupport.Application.Contracts.Response;

public record LoginResponse(
    [property: JsonPropertyName("accessToken")] 
    string AccessToken,
    [property: JsonPropertyName("expiresIn")]
    int ExpiresIn,
    [property: JsonPropertyName("refreshToken")]
    string RefreshToken
);