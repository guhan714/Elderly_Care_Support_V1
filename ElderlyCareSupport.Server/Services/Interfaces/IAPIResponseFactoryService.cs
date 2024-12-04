using ElderlyCareSupport.Server.Common;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IAPIResponseFactoryService
    {
        APIResponseModel<T> CreateResponse<T>(T? data, bool success, string statusMessage, string? errorMessage = null, IEnumerable<Error>? error = null) where T : class;
    }
}
