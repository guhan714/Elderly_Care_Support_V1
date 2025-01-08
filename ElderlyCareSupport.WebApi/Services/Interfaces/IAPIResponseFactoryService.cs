using System.Net;
using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.Contracts;
using ElderlyCareSupport.Server.Contracts.Common;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IApiResponseFactoryService
    {
        ApiResponseModel<T> CreateResponse<T>(T? data, bool success, string statusMessage,HttpStatusCode code, string? errorMessage = null, IEnumerable<Error>? error = null) where T : class;
    }
}
