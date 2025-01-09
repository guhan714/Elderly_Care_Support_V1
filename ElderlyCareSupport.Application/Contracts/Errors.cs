namespace ElderlyCareSupport.Application.Contracts
{
    public class Error
    {
        public Error()
        {
        }

        public Error(string? errorName)
        {
            ErrorName = errorName;
        }

        public string? ErrorName { get; set; } = string.Empty;
    }
}