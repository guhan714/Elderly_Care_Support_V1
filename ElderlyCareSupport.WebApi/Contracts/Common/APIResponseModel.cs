using System.Net;
using ElderlyCareSupport.Server.Common;

namespace ElderlyCareSupport.Server.Contracts.Common
{
    public record ApiResponseModel<T> where T : class
    {
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public IEnumerable<Error>? Errors { get; set; }
    }
}
