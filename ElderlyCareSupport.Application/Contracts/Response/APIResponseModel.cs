using System.Net;

namespace ElderlyCareSupport.Application.Contracts.Response
{
    public record ApiResponseModel<T> where T : class
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public HttpStatusCode StatusCode { get; init; }
        public string StatusMessage { get; init; } 
        public string? ErrorMessage { get; init; }
        public IEnumerable<Error>? Errors { get; init; }
    }
}
