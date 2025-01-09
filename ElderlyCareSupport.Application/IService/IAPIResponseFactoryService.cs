using System.Net;
using ElderlyCareSupport.Application.Contracts;
using ElderlyCareSupport.Application.Contracts.Common;
using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.IService
{
    public interface IApiResponseFactoryService
    {
        ApiResponseModel<T> CreateResponse<T>(T? data, bool success, string statusMessage,HttpStatusCode code, string? errorMessage = null, IEnumerable<Error>? error = null) where T : class;
    }
}
