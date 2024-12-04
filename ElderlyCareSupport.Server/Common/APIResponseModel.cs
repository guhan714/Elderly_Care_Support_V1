namespace ElderlyCareSupport.Server.Common
{
    public class APIResponseModel<T> where T : class
    {
        public bool Success { get; set; }
        public T? Data { get; set; } 
        public int StatusCode { get; set; } 
        public string StatusMessage { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public IEnumerable<Error>? Errors { get; set; }
        public APIResponseModel() 
        {
            Success = true;
        }

    }
}
