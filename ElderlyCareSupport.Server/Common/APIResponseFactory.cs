
using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Common
{
    public class APIResponseFactory : IAPIResponseFactoryService
    {
        public APIResponseModel<T> CreateResponse<T>(T? data, bool success, string statusMessage, string? errorMessage = null, IEnumerable<Error>? error = null) where T : class => new()
        {
            StatusCode = success ? 200 : 400,
            Data = data,
            Success = success,
            StatusMessage = success ? "Ok" : "Can't complete the request",
            ErrorMessage = errorMessage,
            Errors = error
        };

    }
}
