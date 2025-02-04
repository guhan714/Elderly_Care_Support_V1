﻿using System.Net;

namespace ElderlyCareSupport.Application.Contracts.Response;

public class ApiResponseFactory : IApiResponseFactoryService
{
    public ApiResponseModel<T> CreateResponse<T>(T? data, bool success, string statusMessage,HttpStatusCode code, string? errorMessage = null, IEnumerable<Error>? error = null) where T : class
    {
        return new ApiResponseModel<T>
        {
            StatusCode = code,
            Data = data,
            Success = success,
            StatusMessage = success ? "Ok" : "Can't complete the request",
            ErrorMessage = errorMessage,
            Errors = error
        };
    }
}