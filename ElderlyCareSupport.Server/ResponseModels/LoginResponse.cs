﻿using Newtonsoft.Json;

namespace ElderlyCareSupport.Server.ResponseModels;

public class LoginResponse
{
    [JsonProperty(nameof(AccessToken))]
    public string? AccessToken { get; set; }
    [JsonProperty(nameof(ExpiresIn))]
    public int ExpiresIn { get; set; }
    [JsonProperty(nameof(RefreshToken))]
    public string? RefreshToken { get; set; }

}

public class LoginResponseModel
{
    public string? AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }

}