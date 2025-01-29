using System.Net;

namespace ElderlyCareSupport.Application.Contracts.Response
{
    public interface IApiResponseFactoryService
    {
        ApiResponseModel<T> CreateResponse<T>(T? data, bool success, string statusMessage,HttpStatusCode code, string? errorMessage = null, IEnumerable<Error>? error = null) where T : class;
    }
}
