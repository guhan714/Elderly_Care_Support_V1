namespace ElderlyCareSupport.Server.Common
{
    public class APIResponseFactory
    {

        public static APIResponseModel<T> CreateResponse<T>(T? data, bool success, string statusMessage, string? errorMessage = null) where T : class 
        {
            return new APIResponseModel<T> 
            { 
                StatusCode = success ? 200 : 400,
                Data = data, 
                Success = success,
                StatusMessage = success ? "Ok" : "Can't complete the request",
                ErrorMessage = errorMessage,
            };
        }

    }
}
