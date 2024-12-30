using System.Net;

namespace ElderlyCareSupport.Server.Common
{
    public class ApiResponseModel<T> where T : class
    {
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public IEnumerable<Error>? Errors { get; set; }
    }
}
